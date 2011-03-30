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
using ECollegeAPI.Model;

namespace ECollegeAPI.Services
{
    public class FetchTokenService : BaseService
    {
        public Token Result { get; set; }

        public FetchTokenService(String grantToken)
            : base()
        {
            IsAuthenticationRequired = false;
            IsCacheable = false;
            Resource = "authorize/token?access_grant=" + grantToken;
        }

        public override void ProcessResponse(string resp)
        {
            Result = Deserialize<Token>(resp);
        }
    }
}
