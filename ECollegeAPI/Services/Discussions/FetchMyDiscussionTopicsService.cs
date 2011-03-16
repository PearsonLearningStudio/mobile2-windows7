using System;
using System.Collections.Generic;
using ECollegeAPI.Model;
using System.Linq;

namespace ECollegeAPI.Services.Discussions
{
    public class FetchMyDiscussionTopicsService : BaseService
    {
        public List<UserDiscussionTopic> Result { get; set; }

        public FetchMyDiscussionTopicsService(List<long> courseIds)
            : base()
        {
            var courseString = String.Join(";", (from cid in courseIds select (cid + "")).ToArray());
            Resource = "me/userTopics?courses=" + courseString;
        }

        public override void ProcessResponse(RestSharp.RestResponse resp)
        {
            Result = Deserialize<List<UserDiscussionTopic>>(resp, "userTopics");
        }
    }
}