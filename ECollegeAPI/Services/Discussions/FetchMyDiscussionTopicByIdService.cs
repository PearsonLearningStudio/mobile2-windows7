using System.Collections.Generic;
using ECollegeAPI.Model;

namespace ECollegeAPI.Services.Discussions
{
    public class FetchMyDiscussionTopicByIdService : BaseService
    {
        public UserDiscussionTopic Result { get; set; }

        public FetchMyDiscussionTopicByIdService(string userTopicId)
            : base()
        {
            Resource = "me/usertopics/" + userTopicId;
        }

        public override void ProcessResponse(RestSharp.RestResponse resp)
        {
            Result = Deserialize<List<UserDiscussionTopic>>(resp, "userTopics")[0];
        }
    }
}