using System.Collections.Generic;
using ECollegeAPI.Model;

namespace ECollegeAPI.Services.Announcements
{
    public class FetchAnnouncementsService : BaseService
    {
        public List<Announcement> Result { get; set; }

        public FetchAnnouncementsService(long courseId)
            : base()
        {
            Resource = "courses/" + courseId + "/announcements";
        }

        public override void ProcessResponse(RestSharp.RestResponse resp)
        {
            Result = Deserialize<List<Announcement>>(resp, "announcements");
        }
    }
}