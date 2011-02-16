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

namespace eCollegeWP7
{
    public static class UriStrings
    {
    }

    public partial class ECollegeAPI
    {
        
        public const string RootUri = "https://m-api.ecollege.com/";

        readonly string _domain;
        readonly string _username;
        readonly string _password;
        readonly string _epid;
        private Token _currentToken;
        private ECollegeAPIAuthenticator _authenticator;

        public ECollegeAPI(string domain, string username, string password, string epid)
        {
            _domain = domain;
            _username = username;
            _password = password;
            _epid = epid;
        }

        public void ExecuteAsync<T>(RestRequest request, Action<T> callback) where T : new()
        {
            var client = new RestClient(RootUri);
            client.AddHandler("application/json", new CustomJsonDeserializer());

            if (_authenticator != null) client.Authenticator = _authenticator;
            client.ExecuteAsync<T>(request, (response) =>
            {
                var dispatcher = Deployment.Current.Dispatcher;
                dispatcher.BeginInvoke(() =>
                {

                    if (response.ResponseStatus == ResponseStatus.Error)
                    {
                        Debug.WriteLine("ErrorMessage: " + response.ErrorMessage);
                        Debug.WriteLine("ErrorException: \n" + response.ErrorException + "\n");
                        Debugger.Break();
                    }
                    else
                    {
                        callback(response.Data);
                    }


                    //try
                    //{
                    //    var obj = JsonConvert.DeserializeObject<T>(response.Content + "");
                    //    callback(obj);
                    //}
                    //catch (Exception e)
                    //{
                    //    Debugger.Break();
                    //}
                });

            });
        }

        public void ExecuteAsync(RestRequest request, Action<RestResponse> callback)
        {
            var client = new RestClient(RootUri);
            client.AddHandler("application/json", new CustomJsonDeserializer());
            if (_authenticator != null) client.Authenticator = _authenticator;
            client.ExecuteAsync(request, (response) =>
            {
                var dispatcher = Deployment.Current.Dispatcher;
                dispatcher.BeginInvoke(() =>
                {
                    callback(response);
                });

            });
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
                _authenticator = new ECollegeAPIAuthenticator(token.AccessToken);
                callback(token);
            });
        }

        public void FetchMe(Action<AuthenticatedUser> callback)
        {
            var request = new RestRequest("me", Method.GET);
            ExecuteAsync<MeResult>(request, result =>
            {
                callback(result.Me);
            });
        }

        public void FetchMyCourses(Action<List<Course>> callback)
        {
            var request = new RestRequest("me/courses", Method.GET);
            request.AddParameter("expand", "course", ParameterType.GetOrPost);
            ExecuteAsync<CoursesResultList>(request, result =>
            {
                var formattedResult = new List<Course>();
                foreach (var linkContainer in result.Courses)
                {
                    if (linkContainer.Links.Count > 0) {
                        formattedResult.Add(linkContainer.Links[0].Course);
                    }
                }
                callback(formattedResult);
            });
        }

        private class ECollegeAPIAuthenticator : IAuthenticator
        {
            private readonly string _token;

            public ECollegeAPIAuthenticator(string token)
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
