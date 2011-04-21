using System.Collections.Generic;
using ECollegeAPI.Model;
using System.Linq;

namespace ECollegeAPI.Services.Discussions
{
    public class FetchDiscussionTopicsByThreadIdService : BaseService
    {
        public List<UserDiscussionTopic> Result { get; set; }
        private long _threadId;

        public FetchDiscussionTopicsByThreadIdService(long courseId, long threadId)
            : base()
        {
            //Resource = "courses/" + courseId + "/threadeddiscussions/" + threadId + "/topics";
            Resource = "me/userTopics?courses=" + courseId;
            _threadId = threadId;
        }

        public override void ProcessResponse(string resp)
        {
            var rawResult = Deserialize<List<UserDiscussionTopic>>(resp, "userTopics");
            Result = (from t in rawResult where t.Topic.ContainerInfo.ContentItemID == _threadId select t).ToList();
        }
    }
}