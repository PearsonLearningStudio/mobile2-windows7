using System.Collections.Generic;
using ECollegeAPI.Model;
using System;

namespace ECollegeAPI.Services.Activities
{
    public class FetchMyWhatsHappeningFeedService : BaseService
    {
        public List<ActivityStreamItem> Result { get; set; }
        
        public FetchMyWhatsHappeningFeedService() : this(null,null) {}

        public FetchMyWhatsHappeningFeedService(DateTime? since) : this(since,null) {}

        public FetchMyWhatsHappeningFeedService(DateTime? since, long? courseId)
            : base()
        {
            Resource = "me/whatshappeningfeed";

            Resource += "?types=thread-topic,thread-post,grade,dropbox-submission";
            //Resource += "?types=dropbox-submission";

            if (courseId.HasValue)
            {
                Resource += "&courseid=" + courseId;
            }

            if (since.HasValue)
            {
                Resource += "&since=" + since.Value.ToString("MM/dd/yyyy");
            }
        }

        public override void ProcessResponse(RestSharp.RestResponse resp)
        {
            Result = Deserialize<List<ActivityStreamItem>>(resp, "activityStream.items");
        }
    }
}