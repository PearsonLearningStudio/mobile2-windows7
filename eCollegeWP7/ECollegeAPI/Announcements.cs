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
        public void FetchAnnouncements(int courseId, Action<List<Announcement>> callback)
        {
            var request = new RestRequest("courses/" + courseId + "/announcements", Method.GET);
            ExecuteAsync<List<Announcement>>(request, "announcements", result =>
            {
                callback(result);
            });
        }
    }
}
