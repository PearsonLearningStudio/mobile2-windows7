using System;

namespace ECollegeAPI.Model
{
    public class GrantToken
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
    }
}
