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
using System.Security.Cryptography;
using System.Text;

namespace ECollegeAPI.Services
{
    public class BaseService
    {
        private const string CACHE_VERSION = "0"; //change to invalidate existing caches

        public bool IsCacheable { get; set; }
        public bool IsAuthenticationRequired { get; set; }
        public string Resource { get; set; }
        public string CacheScope { get; set; }
        public Method RequestMethod { get; set; }

        public BaseService()
        {
            IsCacheable = true;
            IsAuthenticationRequired = true;
            RequestMethod = Method.GET;
            CacheScope = this.GetType().Namespace; //scope can group related services together so they can be invalidated simultaneously
        }

        public virtual string GetCacheKey()
        {
            return HashUtil.ToSHA1(Resource, RequestMethod.GetType().ToString(), CACHE_VERSION);
        }

        public virtual void PrepareRequest(RestRequest req)
        {

        }

        public virtual void ProcessResponse(string resp)
        {

        }

        protected T Deserialize<T>(string resp) where T : new()
        {
            return Deserialize<T>(resp, null);
        }

        protected T Deserialize<T>(string resp, string rootElement) where T : new()
        {
            var jsonDeserializer = new CustomJsonDeserializer();
            jsonDeserializer.UseISOUniversalTime = true;//parses "2010-10-12T18:03:18Z"

            if (rootElement != null)
            {
                jsonDeserializer.RootElement = rootElement;
            }

            T result = jsonDeserializer.Deserialize<T>(resp);
            return result;
        }


    }
}
