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
        
        public const string RootUri = "https://m-api.ecollege.com/";

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
            ExecuteAsync(request, restresponse => {
                var jsonDeserializer = new CustomJsonDeserializer();
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
            client.AddHandler("application/json", new CustomJsonDeserializer());
            if (_authenticator != null) client.Authenticator = _authenticator;
            client.ExecuteAsync(request, (response) =>
            {
                var paramString = JsonConvert.SerializeObject(request.Parameters, Formatting.Indented);
                Debug.WriteLine("Request: " + request.Method + " - " + RootUri + request.Resource + " - " + paramString);
                Debug.WriteLine("Status: " + response.StatusCode);

                if (response.ContentType.Contains("json")) // == "application/json")
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
                    Debug.WriteLine("ErrorMessage: " + response.ErrorMessage);
                    Debug.WriteLine("ErrorException: \n" + response.ErrorException + "\n");
                    Debugger.Break();
                }

                var dispatcher = Deployment.Current.Dispatcher;
                dispatcher.BeginInvoke(() =>
                {
                    callback(response);
                });

                Debug.WriteLine("\n\n");

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
                _authenticator = new ECollegeClientAuthenticator(token.AccessToken);
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
                    if (linkContainer.Links.Count > 0)
                    {
                        formattedResult.Add(linkContainer.Links[0].Course);
                    }
                }
                callback(formattedResult);
            });
        }

        public void FetchAnnouncements(int courseId, Action<List<Announcement>> callback)
        {
            var request = new RestRequest("courses/" + courseId + "/announcements", Method.GET);
            ExecuteAsync<AnnouncementResultList>(request, result =>
            {
                callback(result.Announcements);
            });
        }


        //public void FetchDiscussions(Action<List<ThreadedDiscussion>> callback)
        //{
        //    //var request = new RestRequest("courses/" + courseId + "/threadeddiscussions", Method.GET);
        //    //request.AddParameter("expand", "topics", ParameterType.GetOrPost);
        //    //ExecuteAsync<ThreadedDiscussionResultList>(request, result =>
        //    //{
        //    //    callback(result.ThreadedDiscussions);
        //    //});
        //}


        //public void FetchDiscussions(int courseId, Action<List<ThreadedDiscussion>> callback)
        //{
        //    var request = new RestRequest("courses/" + courseId + "/threadeddiscussions", Method.GET);
        //    request.AddParameter("expand", "topics", ParameterType.GetOrPost);
        //    ExecuteAsync<ThreadedDiscussionResultList>(request, result =>
        //    {
        //        callback(result.ThreadedDiscussions);
        //    });
        //}

        public class CurrentCoursesResults
        {
            public List<Course> CurrentCourses { get; set; }
        }

        public void FetchMyCurrentCourses(Action<List<Course>> callback)
        {
            var request = new RestRequest("me/currentcourses_moby", Method.GET);
            ExecuteAsync<CurrentCoursesResults>(request, result =>
            {
                callback(result.CurrentCourses);
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
