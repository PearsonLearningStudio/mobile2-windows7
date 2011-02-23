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

        public void FetchMe(Action<User> callback)
        {
            var request = new RestRequest("me", Method.GET);
            ExecuteAsync<User>(request,"me",result =>
            {
                callback(result);
            });
        }

        // Stalled until the enrolledusers api stops returning "Access to that resource is denied"
        public void FetchEnrolledUsers(long courseId, Action<List<EnrolledUser>> callback)
        {
            var request = new RestRequest("courses/" + courseId + "/enrolledUsers", Method.GET);

            ExecuteAsync<List<EnrolledUser>>(request,"enrolledUsers", result =>
            {
                callback(result);
            });
        }
    }
}
