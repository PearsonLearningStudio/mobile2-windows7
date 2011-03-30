using System.Collections.Generic;
using ECollegeAPI.Model;

namespace ECollegeAPI.Services.Discussions
{
    public class FetchMyDiscussionResponseByIdService : BaseService
    {
        public UserDiscussionResponse Result { get; set; }

        public FetchMyDiscussionResponseByIdService(string userResponseId)
            : base()
        {
            Resource = "me/userresponses/" + userResponseId;
        }

        public override void ProcessResponse(string resp)
        {
            Result = Deserialize<List<UserDiscussionResponse>>(resp, "userResponses")[0];
        }
    }
}