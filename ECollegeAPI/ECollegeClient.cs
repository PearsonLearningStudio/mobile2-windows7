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
using System.Collections;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Text;
using System.IO;
using ECollegeAPI.Util;
using ECollegeAPI.Model.Boilerplate;
using ECollegeAPI.Services;

namespace ECollegeAPI
{
    public partial class ECollegeClient
    {
        private class PendingExecuteRequest
        {
            public RestRequest Request { get; set; }
            public Action<RestResponse> Callback { get; set; }
        }

        public const string RootUri = "https://m-api.ecollege.com/";
        //public const string RootUri = "http://ecollegeapiproxy.heroku.com/";
        //public const string RootUri = "http://192.168.1.2:4000/";

        readonly string _clientString;
        readonly string _clientId;

        private bool _loginInProgress = false;
        private Queue<Action> _pendingServiceCalls = new Queue<Action>();

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

        public void ExecuteService<T>(T service, Action<T> successCallback) where T : BaseService
        {
            ExecuteService<T>(service, successCallback, null, null);
        }

        public void ExecuteService<T>(T service, Action<T> successCallback, Action<T, RestResponse> failureCallback) where T : BaseService
        {
            ExecuteService<T>(service, successCallback, failureCallback, null);
        }

        public void ExecuteService<T>(T service, Action<T> successCallback, Action<T,RestResponse> failureCallback, Action<T> finallyCallback) where T : BaseService
        {
            var client = new RestClient(RootUri);
            client.FollowRedirects = true;
            client.MaxRedirects = 10;

            if (service.IsAuthenticationRequired && (_currentToken == null || _currentToken.NeedsToBeRefreshed()))
            {
                if (!_loginInProgress)
                {
                    _loginInProgress = true;
                    PrepareAuthentication();
                }
                _pendingServiceCalls.Enqueue(
                    () => ExecuteService(service, successCallback, failureCallback, finallyCallback));
                return;
            }

            if (_authenticator != null) client.Authenticator = _authenticator;

            var request = new RestRequest(service.Resource,service.RequestMethod);
            service.PrepareRequest(request);

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
                    if (failureCallback != null) failureCallback(service, response);
                    OnClientErrorReturned(response);
                    if (finallyCallback != null) finallyCallback(service);
                }
                else if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.Created && response.StatusCode != HttpStatusCode.NoContent && response.StatusCode != HttpStatusCode.NotModified)
                {
                    if (failureCallback != null) failureCallback(service, response);
                    OnServerErrorReturned(response);
                    if (finallyCallback != null) finallyCallback(service);
                }
                else
                {
                    var dispatcher = Deployment.Current.Dispatcher;
                    dispatcher.BeginInvoke(() =>
                    {
                        service.ProcessResponse(response);
                        successCallback(service);
                        if (finallyCallback != null) finallyCallback(service);
                    });
                }

                Debug.WriteLine("\n\n");

            });

        }

        protected void PrepareAuthentication()
        {
            if (_grantToken == null)
            {
                ExecuteService(new FetchGrantService(_clientString, _clientId, _username, _password), fgs =>
                {
                    _grantToken = fgs.Result.AccessToken;
                    ExecuteService(new FetchTokenService(_grantToken), fts =>
                    {
                        _currentToken = fts.Result;
                        _authenticator = new ECollegeClientAuthenticator(_currentToken.AccessToken);
                        ResumeServices();
                    });
                });
            }
            else
            {
                ExecuteService(new FetchTokenService(_grantToken), fts =>
                {
                    _currentToken = fts.Result;
                    _authenticator = new ECollegeClientAuthenticator(_currentToken.AccessToken);
                    ResumeServices();
                });
            }
        }

        protected void ResumeServices()
        {
            _loginInProgress = false;
            while (_pendingServiceCalls.Count > 0)
            {
                var pendingService = _pendingServiceCalls.Dequeue();
                pendingService();
            }
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
