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
using RestSharp;

namespace ECollegeAPI.Services
{
    public class FetchGrantService : BaseService
    {
        private string _clientString;
        private string _clientId;
        private string _username;
        private string _password;

        public GrantToken Result { get; set; }

        public FetchGrantService(string clientString, string clientId, string username, string password)
            : base()
        {
            this._clientString = clientString;
            this._clientId = clientId;
            this._username = username;
            this._password = password;
            Resource = "authorize/grant";
            RequestMethod = RestSharp.Method.POST;
            IsAuthenticationRequired = false;
            IsCacheable = false;
        }

        public override void PrepareRequest(RestSharp.RestRequest req)
        {
            req.AddParameter("clientString", _clientString, ParameterType.GetOrPost);
            req.AddParameter("client_id", _clientId, ParameterType.GetOrPost);
            req.AddParameter("userLogin", _username, ParameterType.GetOrPost);
            req.AddParameter("password", _password, ParameterType.GetOrPost);
        }

        public override void ProcessResponse(RestSharp.RestResponse resp)
        {
            Result = Deserialize<GrantToken>(resp);
        }

    }
}
