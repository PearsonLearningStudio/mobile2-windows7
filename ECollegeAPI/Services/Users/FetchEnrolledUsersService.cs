using ECollegeAPI.Model;
using System.Collections.Generic;

namespace ECollegeAPI.Services.Users
{
    public class FetchEnrolledUsersService : BaseService
    {
        public List<EnrolledUser> Result { get; set; }

        public FetchEnrolledUsersService(long courseId)
            : base()
        {
            Resource = "courses/" + courseId + "/enrolledUsers";
        }

        public override void ProcessResponse(RestSharp.RestResponse resp)
        {
            Result = Deserialize<List<EnrolledUser>>(resp, "enrolledUsers");
        }
    }
}
