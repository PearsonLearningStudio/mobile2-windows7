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
using Newtonsoft.Json.Linq;

namespace ECollegeAPI
{
    public partial class ECollegeClient
    {

        public void FetchMyDiscussionTopics(List<long> courseIds, Action<List<UserDiscussionTopic>> callback)
        {
            var courseString = String.Join(";", (from cid in courseIds select (cid + "")).ToArray());
            var request = new RestRequest("me/userTopics?courses=" + courseString, Method.GET);

            ExecuteAsync<List<UserDiscussionTopic>>(request, "userTopics", result =>
            {
                callback(result);
            });
        }

        public void FetchMyDiscussionResponsesByTopic(string topicId, Action<List<UserDiscussionResponse>> callback)
        {
            var request = new RestRequest("me/topics/" + topicId + "/userresponses", Method.GET);

            ExecuteAsync<List<UserDiscussionResponse>>(request,"userResponses", result =>
            {
                callback(result);
            });
        }

        public void FetchMyDiscussionResponsesByResponse(string responseId, Action<List<UserDiscussionResponse>> callback)
        {
            var request = new RestRequest("me/responses/" + responseId + "/userresponses", Method.GET);

            ExecuteAsync<List<UserDiscussionResponse>>(request,"userResponses", result =>
            {
                callback(result);
            });
        }

        public void FetchMyDiscussionTopicById(string userTopicId, Action<UserDiscussionTopic> callback)
        {
            var request = new RestRequest("me/usertopics/" + userTopicId, Method.GET);

            ExecuteAsync<List<UserDiscussionTopic>>(request, "userTopics", result =>
            {
                callback(result.First());
            });
        }

        public void FetchMyDiscussionResponseById(string userResponseId, Action<UserDiscussionResponse> callback)
        {
            var request = new RestRequest("me/userresponses/" + userResponseId, Method.GET);

            ExecuteAsync<List<UserDiscussionResponse>>(request,"userResponses", result =>
            {
                callback(result.First());
            });
        }

        public void PostMyResponseToTopic(string topicId, string responseTitle, string responseText, Action<RestResponse> callback)
        {
            var request = new RestRequest("me/topics/" + topicId + "/responses", Method.POST);

            JObject postData = new JObject();
            JObject postDataResponses = new JObject();
            postDataResponses["title"] = responseTitle;
            postDataResponses["description"] = responseText;
            postData["responses"] = postDataResponses;
            request.AddParameter("RequestBody", postData.ToString(), ParameterType.RequestBody);

            ExecuteAsync(request, result =>
            {
                callback(result);
            });
        }

        public void PostMyResponseToResponse(string responseId, string responseTitle, string responseText, Action<RestResponse> callback)
        {
            var request = new RestRequest("me/responses/" + responseId + "/responses", Method.POST);

            JObject postData = new JObject();
            JObject postDataResponses = new JObject();
            postDataResponses["title"] = responseTitle;
            postDataResponses["description"] = responseText;
            postData["responses"] = postDataResponses;
            request.AddParameter("RequestBody", postData.ToString(), ParameterType.RequestBody);

            ExecuteAsync(request, result =>
            {
                callback(result);
            });
        }


    }
}
