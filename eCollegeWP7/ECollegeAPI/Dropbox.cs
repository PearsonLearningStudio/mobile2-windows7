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
using ECollegeAPI.Model;
using System.Collections.Generic;
using RestSharp;
using ECollegeAPI.Model.Boilerplate;

namespace ECollegeAPI
{
    public partial class ECollegeClient
    {

        // Stalled until the enrolledusers api stops returning "Access to that resource is denied"
        public void FetchDropboxBaskets(long courseId, Action<List<DropboxBasket>> callback)
        {
            var request = new RestRequest("courses/" + courseId + "/dropboxBaskets", Method.GET);

            ExecuteAsync<List<DropboxBasket>>(request,"dropboxBaskets", result =>
            {
                callback(result);
            });
        }

        public void FetchDropboxMessages(long studentId, long courseId, long basketId, Action<List<DropboxMessage>> callback)
        {
            var request = new RestRequest("courses/" + courseId + "/dropboxBaskets/" + basketId + "/messages", Method.GET);

            request.AddParameter("submissionStudents", Convert.ToString(studentId), ParameterType.GetOrPost);

            ExecuteAsync<List<DropboxMessage>>(request,"messages", result =>
            {
                callback(result);
            });
        }

        public void FetchDropboxMessage(long courseId, long basketId, long messageId, Action<DropboxMessage> callback)
        {
            var request = new RestRequest("courses/" + courseId + "/dropboxBaskets/" + basketId + "/messages/" + messageId, Method.GET);

            ExecuteAsync<List<DropboxMessage>>(request, "messages", result =>
            {
                callback(result[0]);
            });
        }
    }
}
