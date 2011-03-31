using System;
using System.Collections.Generic;
using ECollegeAPI.Model;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace ECollegeAPI.Services.Discussions
{
    public class UpdateResponseReadStatusService : BaseService
    {
        private bool _markedAsRead = true;

        public UpdateResponseReadStatusService(long responseId) : this(responseId, true) { }

        public UpdateResponseReadStatusService(long responseId, bool markedAsRead)
            : base()
        {
            Resource = "me/responses/" + responseId + "/readStatus";
            RequestMethod = Method.POST;
            IsCacheable = false;
            _markedAsRead = markedAsRead;
        }

        public override void PrepareRequest(RestRequest req)
        {
            var postData = new JObject();
            var postDataReadStatus = new JObject();
            postDataReadStatus["markedAsRead"] = _markedAsRead;
            postData["readStatus"] = postDataReadStatus;
            req.AddParameter("RequestBody", postData.ToString(), ParameterType.RequestBody);
        }

    }
}