using System.Collections.Generic;
using ECollegeAPI.Model;
using System;

namespace ECollegeAPI.Services.Activities
{
    public class FetchMyWhatsHappeningFeedService : BaseService
    {
        public List<ActivityStreamItem> Result { get; set; }
        
        public FetchMyWhatsHappeningFeedService() : this(null,null,null) {}

        public FetchMyWhatsHappeningFeedService(DateTime? since) : this(since,null,null) {}

        public FetchMyWhatsHappeningFeedService(DateTime? since, long? courseId, string typeFilter)
            : base()
        {
            Resource = "me/whatshappeningfeed";

            if (typeFilter != null)
            {
                Resource += "?types=" + typeFilter;
            } else
            {
                Resource += "?types=thread-topic,thread-post,grade,dropbox-submission";
                //Resource += "?types=dropbox-submission";
            }

            if (courseId.HasValue)
            {
                Resource += "&courseid=" + courseId;
            }

            if (since.HasValue)
            {
                Resource += "&since=" + since.Value.ToString("MM/dd/yyyy");
            }
        }

        public override void ProcessResponse(string resp)
        {
            Result = Deserialize<List<ActivityStreamItem>>(resp, "activityStream.items");
        }
    }
}