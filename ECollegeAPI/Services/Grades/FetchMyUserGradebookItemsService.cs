using System.Collections.Generic;
using ECollegeAPI.Model;

namespace ECollegeAPI.Services.Grades
{
    public class FetchMyUserGradebookItemsService : BaseService
    {
        public List<UserGradebookItem> Result { get; set; }

        public FetchMyUserGradebookItemsService(long courseId)
            : base()
        {
            Resource = "/me/courses/" + courseId + "/userGradebookItems?expand=grade";
        }

        public override void ProcessResponse(string resp)
        {
            Result = Deserialize<List<UserGradebookItem>>(resp, "userGradebookItems");
        }
    }
}