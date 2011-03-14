using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using RestSharp;
using ECollegeAPI.Model;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Text;
using System.IO;
using eCollegeWP7.Util;
using ECollegeAPI.Model;
using ECollegeAPI.Model.Boilerplate;

namespace ECollegeAPI
{
    public partial class ECollegeClient
    {
        private class PendingExecuteRequest
        {
            public RestRequest Request { get; set; }
            public Action<RestResponse> Callback { get; set; }
        }

        //public const string RootUri = "https://m-api.ecollege.com/";
        public const string RootUri = "http://ecollegeapiproxy.heroku.com/";
        //public const string RootUri = "http://192.168.1.2:4000/";

        readonly string _clientString;
        readonly string _clientId;

        private bool _loginInProgress = false;
        private Queue<PendingExecuteRequest> _pendingRequests = new Queue<PendingExecuteRequest>();

        private string _username;
        private string _password;
        private string _grantToken;

        private Token _currentToken;
        private ECollegeClientAuthenticator _authenticator;

        public string GrantToken { get { return _grantToken; } }


        public ECollegeClient(string clientString, string clientId)
        {
            this._clientString = clientString;
            this._clientId = clientId;
        }

        public void SetupAuthentication(string username, string password)
        {
            this._username = username;
            this._password = password;
        }

        public void SetupAuthentication(string grantToken)
        {
            this._grantToken = grantToken;
        }

        protected string PrettyPrint(string json)
        {
            object jsonObject = JsonConvert.DeserializeObject(json);
            return JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
        }

        public void ExecuteAsync<T>(RestRequest request, Action<T> callback) where T : new()
        {
            ExecuteAsync(request, true, null, callback);
        }

        public void ExecuteAsync<T>(RestRequest request, string rootElement, Action<T> callback) where T : new()
        {
            ExecuteAsync<T>(request, true, rootElement, callback);
        }

        public void ExecuteAsync<T>(RestRequest request, bool needsAuthentication, string rootElement, Action<T> callback) where T : new()
        {
            ExecuteAsync(request,needsAuthentication, restresponse => {
                var jsonDeserializer = new CustomJsonDeserializer();

                if (rootElement != null)
                {
                    jsonDeserializer.RootElement = rootElement;
                }

                try {
                    T result = jsonDeserializer.Deserialize<T>(restresponse);
                    callback(result);
                } catch (Exception e) {
                    Debugger.Break();
                }
            });
        }

        protected void PrepareAuthentication()
        {

            if (_grantToken == null)
            {
                FetchGrant((gtok) =>
                {
                    FetchToken((tok) =>
                    {
                        ResumeRequests();
                    });
                });
            }
            else
            {
                FetchToken((tok) =>
                {
                    ResumeRequests();
                });
            }

        }

        protected void ResumeRequests()
        {
            _loginInProgress = false;
            while (_pendingRequests.Count > 0)
            {
                var pending = _pendingRequests.Dequeue();
                ExecuteAsync(pending.Request, pending.Callback);
            }
        }

        public void ExecuteAsync(RestRequest request, Action<RestResponse> callback)
        {
            ExecuteAsync(request, true, callback);
        }

        public void ExecuteAsync(RestRequest request, bool needsAuthentication, Action<RestResponse> callback)
        {
            var client = new RestClient(RootUri);
            client.FollowRedirects = true;
            client.MaxRedirects = 10;

            if (needsAuthentication && (_currentToken == null || _currentToken.NeedsToBeRefreshed()))
            {
                if (!_loginInProgress)
                {
                    _loginInProgress = true;
                    PrepareAuthentication();
                }
                _pendingRequests.Enqueue(new PendingExecuteRequest
                {
                    Request = request,
                    Callback = callback
                });
                return;
            }

            if (_authenticator != null) client.Authenticator = _authenticator;
            client.ExecuteAsync(request, (response) =>
            {
                var paramString = JsonConvert.SerializeObject(request.Parameters, Formatting.Indented);
                Debug.WriteLine("Request: " + request.Method + " - " + RootUri + request.Resource + " - " + paramString);
                Debug.WriteLine("Status: " + response.StatusCode);

                if (response.ContentType != null && response.ContentType.Contains("json")) // == "application/json")
                {
                    var prettyResponse = PrettyPrint(response.Content);
                    Debug.WriteLine("Response: " + prettyResponse + "\n");
                }
                else
                {
                    Debug.WriteLine("Response (" + response.ContentType + "): " + response.Content + "\n");
                }

                if (response.ResponseStatus == ResponseStatus.Error)
                {
                    OnClientErrorReturned(response);
                }
                else if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.Created && response.StatusCode != HttpStatusCode.NoContent && response.StatusCode != HttpStatusCode.NotModified)
                {
                    OnServerErrorReturned(response);
                }
                else
                {
                    var dispatcher = Deployment.Current.Dispatcher;
                    dispatcher.BeginInvoke(() =>
                    {
                        callback(response);
                    });
                }

                Debug.WriteLine("\n\n");

            });
        }

        protected void OnClientErrorReturned(RestResponse response)
        {
            Debug.WriteLine("ErrorMessage: " + response.ErrorMessage);
            Debug.WriteLine("ErrorException: \n" + response.ErrorException + "\n");
            Debugger.Break();
        }

        protected void OnServerErrorReturned(RestResponse response)
        {
            Debug.WriteLine("ERROR!" + response.StatusCode);
            Debugger.Break();
        }

        public void FetchToken(Action<Token> callback)
        {
            var request = new RestRequest("authorize/token?access_grant=" + _grantToken,Method.GET);
            ExecuteAsync<Token>(request,false,null,(token) =>
            {
                _currentToken = token;
                _authenticator = new ECollegeClientAuthenticator(token.AccessToken);
                callback(token);
            });
        }

        public void FetchGrant(Action<GrantToken> callback)
        {
            var request = new RestRequest("authorize/grant", Method.POST);
            request.AddParameter("clientString", _clientString, ParameterType.GetOrPost);
            request.AddParameter("client_id", _clientId, ParameterType.GetOrPost);
            request.AddParameter("userLogin", _username, ParameterType.GetOrPost);
            request.AddParameter("password", _password, ParameterType.GetOrPost);

            ExecuteAsync<GrantToken>(request,false,null,(gtoken) =>
            {
                _grantToken = gtoken.AccessToken;
                callback(gtoken);
            });
        }

        private class ECollegeClientAuthenticator : IAuthenticator
        {
            private readonly string _token;

            public ECollegeClientAuthenticator(string token)
            {
                _token = token;
            }

            public void Authenticate(RestClient client, RestRequest request)
            {
                request.AddHeader("X-Authorization", "Access_Token access_token=" + _token);
            }

        }

    }
}
