using System.Collections.Generic;
using ECollegeAPI.Model;

namespace ECollegeAPI.Services.Grades
{
    public class FetchMyGradebookItemGradeService : BaseService
    {
        public Grade Result { get; set; }

        public FetchMyGradebookItemGradeService(long courseId, string gradebookItemGuid)
            : base()
        {
            Resource = "me/courses/" + courseId + "/gradebookItems/" + gradebookItemGuid + "/grade";
        }

        public override void ProcessResponse(string resp)
        {
            Result = Deserialize<Grade>(resp, "grade");
        }
    }
}