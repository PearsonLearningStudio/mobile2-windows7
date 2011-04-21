using System.Collections.Generic;
using ECollegeAPI.Model;

namespace ECollegeAPI.Services.Discussions
{
    public class FetchDiscussionThreadByIdService : BaseService
    {
        public DiscussionThread Result { get; set; }

        public FetchDiscussionThreadByIdService(long courseId, long threadId)
            : base()
        {
            Resource = "courses/" + courseId + "/threadeddiscussions/" + threadId;
        }

        public override void ProcessResponse(string resp)
        {
            Result = Deserialize<List<DiscussionThread>>(resp, "threadedDiscussions")[0];
        }
    }
}