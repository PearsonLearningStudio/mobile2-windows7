using System;
using System.Collections.Generic;
using ECollegeAPI.Model;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace ECollegeAPI.Services.Discussions
{
    public class PostMyResponseToResponseService : BaseService
    {
        private string _responseTitle;
        private string _responseText;

        public PostMyResponseToResponseService(long responseId, string responseTitle, string responseText)
            : base()
        {
            this._responseTitle = responseTitle;
            this._responseText = responseText;
            Resource = "me/responses/" + responseId + "/responses";
            RequestMethod = Method.POST;
            IsCacheable = false;
        }

        public override void PrepareRequest(RestRequest req)
        {
            var postData = new JObject();
            var postDataResponses = new JObject();
            postDataResponses["title"] = _responseTitle;
            postDataResponses["description"] = _responseText;
            postData["responses"] = postDataResponses;
            req.AddParameter("RequestBody", postData.ToString(), ParameterType.RequestBody);
        }

    }
}