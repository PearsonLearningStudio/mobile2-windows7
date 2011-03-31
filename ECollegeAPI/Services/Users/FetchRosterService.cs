using ECollegeAPI.Model;
using System.Collections.Generic;

namespace ECollegeAPI.Services.Users
{
    public class FetchRosterService : BaseService
    {
        public List<RosterUser> Result { get; set; }

        public FetchRosterService(long courseId)
            : base()
        {
            Resource = "courses/" + courseId + "/roster";
        }

        public override void ProcessResponse(string resp)
        {
            Result = Deserialize<List<RosterUser>>(resp, "roster");
        }
    }
}
