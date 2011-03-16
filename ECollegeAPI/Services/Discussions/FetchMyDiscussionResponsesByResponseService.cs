using System.Collections.Generic;
using ECollegeAPI.Model;

namespace ECollegeAPI.Services.Discussions
{
    public class FetchMyDiscussionResponsesByResponseService : BaseService
    {
        public List<UserDiscussionResponse> Result { get; set; }

        public FetchMyDiscussionResponsesByResponseService(string responseId)
            : base()
        {
            Resource = "me/responses/" + responseId + "/userresponses";
        }

        public override void ProcessResponse(RestSharp.RestResponse resp)
        {
            Result = Deserialize<List<UserDiscussionResponse>>(resp, "userResponses");
        }
    }
}