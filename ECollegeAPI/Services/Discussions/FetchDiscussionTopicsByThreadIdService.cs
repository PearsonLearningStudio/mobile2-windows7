using System.Collections.Generic;
using ECollegeAPI.Model;

namespace ECollegeAPI.Services.Discussions
{
    public class FetchDiscussionTopicsByThreadIdService : BaseService
    {
        public List<DiscussionThreadTopic> Result { get; set; }

        public FetchDiscussionTopicsByThreadIdService(long courseId, long threadId)
            : base()
        {
            Resource = "courses/" + courseId + "/threadeddiscussions/" + threadId + "/topics";
        }

        public override void ProcessResponse(string resp)
        {
            Result = Deserialize<List<DiscussionThreadTopic>>(resp, "topics");
        }
    }
}