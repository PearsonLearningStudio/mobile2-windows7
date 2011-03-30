using System.Collections.Generic;
using ECollegeAPI.Model;
using RestSharp;

namespace ECollegeAPI.Services.Courses
{
    public class FetchMyCurrentCoursesService : BaseService
    {
        public List<Course> Result { get; set; }

        public FetchMyCurrentCoursesService()
            : base()
        {
            Resource = "me/currentcourses_moby";
        }

        public override void ProcessResponse(string resp)
        {
            Result = Deserialize<List<Course>>(resp, "currentCourses");
        }
    }
}