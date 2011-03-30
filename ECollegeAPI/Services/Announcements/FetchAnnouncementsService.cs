using System.Collections.Generic;
using ECollegeAPI.Model;

namespace ECollegeAPI.Services.Announcements
{
    public class FetchAnnouncementsService : BaseService
    {
        public List<Announcement> Result { get; set; }

        public FetchAnnouncementsService(long courseId)
            : this(courseId,true)
        {
        }

        public FetchAnnouncementsService(long courseId, bool excludeInactive)
            : base()
        {
            Resource = "courses/" + courseId + "/announcements";
            if (excludeInactive)
            {
                Resource += "?excludeInactive=true";
            }
        }

        public override void ProcessResponse(string resp)
        {
            Result = Deserialize<List<Announcement>>(resp, "announcements");
        }
    }
}