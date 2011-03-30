using System.Collections.Generic;
using ECollegeAPI.Model;

namespace ECollegeAPI.Services.Dropbox
{
    public class FetchDropboxBasketsService : BaseService
    {
        public List<DropboxBasket> Result { get; set; }

        public FetchDropboxBasketsService(long courseId)
            : base()
        {
            Resource = "courses/" + courseId + "/dropboxBaskets";
        }

        public override void ProcessResponse(string resp)
        {
            Result = Deserialize<List<DropboxBasket>>(resp, "dropboxBaskets");
        }
    }
}