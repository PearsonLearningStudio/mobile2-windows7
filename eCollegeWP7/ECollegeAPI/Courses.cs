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
        public void FetchMyCourses(Action<List<Course>> callback)
        {
            var request = new RestRequest("me/courses", Method.GET);
            request.AddParameter("expand", "course", ParameterType.GetOrPost);
            ExecuteAsync<List<LinkContainer<CourseLink>>>(request, "courses", result =>
            {
                var formattedResult = new List<Course>();
                foreach (var linkContainer in result)
                {
                    if (linkContainer.Links.Count > 0)
                    {
                        formattedResult.Add(linkContainer.Links[0].Course);
                    }
                }
                callback(formattedResult);
            });
        }

        public void FetchMyCurrentCourses(Action<List<Course>> callback)
        {
            var request = new RestRequest("me/currentcourses_moby", Method.GET);
            ExecuteAsync<List<Course>>(request, "currentCourses", result =>
            {
                callback(result);
            });
        }

    }
}
