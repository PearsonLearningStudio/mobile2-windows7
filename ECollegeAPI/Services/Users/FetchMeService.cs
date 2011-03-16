using ECollegeAPI.Model;

namespace ECollegeAPI.Services.Users
{
    public class FetchMeService : BaseService
    {
        public User Result { get; set; }

        public FetchMeService()
            : base()
        {
            Resource = "/me";
        }

        public override void ProcessResponse(RestSharp.RestResponse resp)
        {
            Result = Deserialize<User>(resp,"me");
        }
    }
}
