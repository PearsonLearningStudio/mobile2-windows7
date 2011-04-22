using System.Collections.Generic;
using ECollegeAPI.Model;
using System;

namespace ECollegeAPI.Services.Upcoming
{
    public class FetchMyUpcomingEventsService : BaseService
    {
        public List<UpcomingEventItem> Result { get; set; }
        
        public FetchMyUpcomingEventsService() : this(null) {}

        public FetchMyUpcomingEventsService(DateTime? until)
        {
            Resource = "me/upcomingevents";
            if (until.HasValue)
            {
                Resource += "?until=" + until.Value.ToString("MM/dd/yyyy");
            }
        }

        public override void ProcessResponse(string resp)
        {
            Result = Deserialize<List<UpcomingEventItem>>(resp, "upcomingEvents");
        }
    }
}