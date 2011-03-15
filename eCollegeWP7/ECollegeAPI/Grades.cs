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
        public void FetchGradebookItemByGuid(long courseId, string gradebookItemGuid, Action<GradebookItem> callback)
        {
            var request = new RestRequest("courses/" + courseId + "/gradebookItems/" + gradebookItemGuid, Method.GET);
            ExecuteAsync<List<GradebookItem>>(request, "gradebookItems", result =>
            {
                callback(result.First());
            });
        }

        public void FetchMyGradebookItemGrade(long courseId, string gradebookItemGuid, Action<Grade> callback)
        {
            var request = new RestRequest("me/courses/" + courseId + "/gradebookItems/" + gradebookItemGuid + "/grade", Method.GET);
            ExecuteAsync<Grade>(request, "grade", result =>
            {
                callback(result);
            });
        }

    }
}
