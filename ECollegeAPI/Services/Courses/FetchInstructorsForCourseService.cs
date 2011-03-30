using System.Collections.Generic;
using ECollegeAPI.Model;
using RestSharp;

namespace ECollegeAPI.Services.Courses
{
    public class FetchInstructorsForCourseService : BaseService
    {
        public List<User> Result { get; set; }

        public FetchInstructorsForCourseService(long courseId)
            : base()
        {
            Resource = "/courses/" + courseId + "/instructors";
        }

        public override void ProcessResponse(string resp)
        {
            Result = Deserialize<List<User>>(resp, "instructors");
        }
    }

}