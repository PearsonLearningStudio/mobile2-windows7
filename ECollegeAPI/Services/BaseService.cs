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
using System.Diagnostics;
using ECollegeAPI.Util;

namespace ECollegeAPI.Services
{
    public class BaseService
    {
        public bool IsCacheable { get; set; }
        public bool IsAuthenticationRequired { get; set; }
        public string Resource { get; set; }
        public Method RequestMethod { get; set; }

        public BaseService()
        {
            IsCacheable = true;
            IsAuthenticationRequired = true;
            RequestMethod = Method.GET;
        }

        public virtual void PrepareRequest(RestRequest req)
        {

        }

        public virtual void ProcessResponse(RestResponse resp)
        {

        }

        protected T Deserialize<T>(RestResponse resp) where T : new()
        {
            return Deserialize<T>(resp, null);
        }

        protected T Deserialize<T>(RestResponse resp, string rootElement) where T : new()
        {
            var jsonDeserializer = new CustomJsonDeserializer();

            if (rootElement != null)
            {
                jsonDeserializer.RootElement = rootElement;
            }

            T result = jsonDeserializer.Deserialize<T>(resp);
            return result;
        }


    }
}
