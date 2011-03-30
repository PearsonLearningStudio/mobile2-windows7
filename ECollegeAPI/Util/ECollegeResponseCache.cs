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

namespace ECollegeAPI.Util
{

    public class ECollegeResponseCacheEntry
    {
        public string Data { get; set; }
        public DateTime CachedAt { get; set; }
    }

    public interface ECollegeResponseCache
    {
        ECollegeResponseCacheEntry Get(string cacheKey);
        void Put(string cacheKey, string responseContent);
    }
}
