using System;

namespace ECollegeAPI.Model
{
    public class Token
    {
        private string _RefreshToken = string.Empty;
        private string _AccessToken = string.Empty;
        private double _ExpiresIn = 0;
        private DateTime _Expires = DateTime.Now;

        public string RefreshToken
        {
            get { return _RefreshToken; }
            set { _RefreshToken = value; }
        }

        public string AccessToken
        {
            get { return _AccessToken; }
            set { _AccessToken = value; }
        }

        public double ExpiresIn
        {
            get { return _ExpiresIn; }
            set
            {
                Expires = DateTime.Now.AddSeconds(value);
                _ExpiresIn = value;
            }
        }

        public DateTime Expires
        {
            get { return _Expires; }
            set { _Expires = value; }
        }
    }
}
