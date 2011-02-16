using System;
using System.Runtime.Serialization;

namespace eCollegeWP7.ViewModels.DataContract
{
    [DataContract]
    public class Token
    {
        private string _RefreshToken = string.Empty;
        private string _AccessToken = string.Empty;
        private double _ExpiresIn = 0;
        private DateTime _Expires = DateTime.Now;

        [DataMember(Name = "refresh_token")]
        public string RefreshToken
        {
            get { return _RefreshToken; }
            set { _RefreshToken = value; }
        }

        [DataMember(Name = "access_token")]
        public string AccessToken
        {
            get { return _AccessToken; }
            set { _AccessToken = value; }
        }

        [DataMember(Name = "expires_in")]
        public double ExpiresIn
        {
            get { return _ExpiresIn; }
            set
            {

                Expires = DateTime.Now.AddSeconds(value);
                _ExpiresIn = value;
            }
        }

        [DataMember(Name = "expires", IsRequired = false)]
        public DateTime Expires
        {
            get { return _Expires; }
            set { _Expires = value; }
        }
    }
}
