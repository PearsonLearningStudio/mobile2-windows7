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
    public static class UriStrings
    {
    }

    public partial class ECollegeClient
    {

        //public const string RootUri = "https://m-api.ecollege.com/";
        public const string RootUri = "http://ecollegeapiproxy.heroku.com/";
        //public const string RootUri = "http://192.168.1.2:4000/";

        readonly string _domain;
        readonly string _username;
        readonly string _password;
        readonly string _epid;
        private Token _currentToken;
        private ECollegeClientAuthenticator _authenticator;
        public Token CurrentToken { get { return _currentToken; } }

        public ECollegeClient(Token token)
        {
            _currentToken = token;
            _authenticator = new ECollegeClientAuthenticator(token.AccessToken);
        }

        public ECollegeClient(string domain, string username, string password, string epid)
        {
            _domain = domain;
            _username = username;
            _password = password;
            _epid = epid;
        }

        protected string PrettyPrint(string json)
        {
            object jsonObject = JsonConvert.DeserializeObject(json);
            return JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
        }


        public void ExecuteAsync<T>(RestRequest request, Action<T> callback) where T : new()
        {
            ExecuteAsync(request, null, callback);
        }

        public void ExecuteAsync<T>(RestRequest request, string rootElement, Action<T> callback) where T : new()
        {
            ExecuteAsync(request, restresponse => {
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

        public void ExecuteAsync(RestRequest request, Action<RestResponse> callback)
        {
            var client = new RestClient(RootUri);
            client.FollowRedirects = true;
            client.MaxRedirects = 10;
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
            var request = new RestRequest("token",Method.POST);
            request.AddParameter("grant_type", "password",ParameterType.GetOrPost);
            request.AddParameter("username", _domain + "\\" + _username, ParameterType.GetOrPost);
            request.AddParameter("password", _password, ParameterType.GetOrPost);
            request.AddParameter("client_id", _epid, ParameterType.GetOrPost);
            ExecuteAsync<Token>(request, (token) =>
            {
                _currentToken = token;
                _authenticator = new ECollegeClientAuthenticator(token.AccessToken);
                callback(token);
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
