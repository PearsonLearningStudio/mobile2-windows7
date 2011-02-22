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
using System.Linq;

namespace ECollegeAPI
{
    public partial class ECollegeClient
    {

        public void FetchMyDiscussionTopics(List<long> courseIds, Action<List<DiscussionTopicHeader>> callback)
        {
            var courseString = String.Join(";", (from cid in courseIds select (cid + "")).ToArray());
            var request = new RestRequest("me/userTopics?courses=" + courseString, Method.GET);

            ExecuteAsync<UserTopicsResultList>(request, result =>
            {
                callback(result.UserTopics);
            });
        }

        public void FetchMyDiscussionResponsesByTopic(string topicId, Action<List<DiscussionResponseHeader>> callback)
        {
            var request = new RestRequest("me/topics/" + topicId + "/userresponses", Method.GET);

            ExecuteAsync<UserResponsesResultList>(request, result =>
            {
                callback(result.UserResponses);
            });
        }

        public void FetchMyDiscussionResponsesByResponse(string responseId, Action<List<DiscussionResponseHeader>> callback)
        {
            var request = new RestRequest("me/responses/" + responseId + "/userresponses", Method.GET);

            ExecuteAsync<UserResponsesResultList>(request, result =>
            {
                callback(result.UserResponses);
            });
        }

        public void FetchMyDiscussionTopicById(string topicHeaderId, Action<DiscussionTopicHeader> callback)
        {
            var request = new RestRequest("me/usertopics/" + topicHeaderId, Method.GET);

            ExecuteAsync<UserTopicsResultList>(request, result =>
            {
                callback(result.UserTopics.First());
            });
        }

        public void FetchMyDiscussionResponseById(string responseHeaderId, Action<DiscussionResponseHeader> callback)
        {
            var request = new RestRequest("me/userresponses/" + responseHeaderId, Method.GET);

            ExecuteAsync<UserResponsesResultList>(request, result =>
            {
                callback(result.UserResponses.First());
            });
        }

        public void PostMyResponseToTopic(string topicId, string responseText, Action<RestResponse> callback)
        {
            var request = new RestRequest("me/topics/" + topicId + "/responses", Method.POST);

        }


    }
}
