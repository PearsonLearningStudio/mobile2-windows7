using System.Collections.Generic;
using ECollegeAPI.Model;

namespace ECollegeAPI.Services.Announcements
{
    public class FetchAnnouncementService : BaseService
    {
        public Announcement Result { get; set; }

        public FetchAnnouncementService(long courseId, long announcementId)
            : base()
        {
            Resource = "courses/" + courseId + "/announcements/" + announcementId;
        }

        public override void ProcessResponse(string resp)
        {
            Result = Deserialize<List<Announcement>>(resp, "announcements")[0];
        }
    }
}