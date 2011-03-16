using System.Collections.Generic;
using ECollegeAPI.Model;

namespace ECollegeAPI.Services.Dropbox
{
    public class FetchDropboxMessagesService : BaseService
    {
        public List<DropboxMessage> Result { get; set; }

        public FetchDropboxMessagesService(long studentId, long courseId, long basketId)
            : base()
        {
            Resource = "courses/" + courseId + "/dropboxBaskets/" + basketId + "/messages";
            Resource += "?submissionStudents=" + studentId;
        }

        public override void ProcessResponse(RestSharp.RestResponse resp)
        {
            Result = Deserialize<List<DropboxMessage>>(resp, "messages");
        }
    }
}