using System;

namespace ECollegeAPI.Model
{
    public class GrantToken
    {
        public string AccessToken { get; set; }
        public DateTime ExpiresIn { get; set; }
    }
}
