using System.Collections.Generic;
using ECollegeAPI.Model;

namespace ECollegeAPI.Services.Grades
{
    public class FetchGradebookItemByGuidService : BaseService
    {
        public GradebookItem Result { get; set; }

        public FetchGradebookItemByGuidService(long courseId, string gradebookItemGuid)
            : base()
        {
            Resource = "courses/" + courseId + "/gradebookItems/" + gradebookItemGuid;
        }

        public override void ProcessResponse(RestSharp.RestResponse resp)
        {
            Result = Deserialize<List<GradebookItem>>(resp, "gradebookItems")[0];
        }
    }
}