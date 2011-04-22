using System.Collections.Generic;
using ECollegeAPI.Model;
using System;

namespace ECollegeAPI.Services.Multimedia
{
    public class FetchHtmlByIdService : BaseService
    {
        public string Result { get; set; }

        public FetchHtmlByIdService(long courseId, long htmlId)
        {
            Resource = "courses/" + courseId + "/textMultimedias/" + htmlId + "/content.html";
        }

        public override void ProcessResponse(string resp)
        {
            Result = resp;
        }
    }
}