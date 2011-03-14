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
    public partial class ECollegeClient {
    
        public void FetchMyWhatsHappeningFeed(Action<List<ActivityStreamItem>> callback)
        {
            FetchMyWhatsHappeningFeed(null,callback);
        }

        public void FetchMyWhatsHappeningFeed(DateTime? since, Action<List<ActivityStreamItem>> callback)
        {
            string resource = "me/whatshappeningfeed";

            if (since.HasValue) {
                resource += "?since=" + since.Value.ToString("MM/dd/yyyy");
            }

            var request = new RestRequest(resource, Method.GET);
            ExecuteAsync<List<ActivityStreamItem>>(request, "activityStream.items", result =>
            {
                callback(result);
            });
        }
    }
}
