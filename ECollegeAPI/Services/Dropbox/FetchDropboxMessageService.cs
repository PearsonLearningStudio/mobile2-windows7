using System.Collections.Generic;
using ECollegeAPI.Model;

namespace ECollegeAPI.Services.Dropbox
{
    public class FetchDropboxMessageService : BaseService
    {
        public DropboxMessage Result { get; set; }

        public FetchDropboxMessageService(long courseId, long basketId, long messageId)
            : base()
        {
            Resource = "courses/" + courseId + "/dropboxBaskets/" + basketId + "/messages/" + messageId;
        }

        public override void ProcessResponse(string resp)
        {
            Result = Deserialize<List<DropboxMessage>>(resp, "messages")[0];
        }
    }
}