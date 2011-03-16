using System.Collections.Generic;
using ECollegeAPI.Model;
using ECollegeAPI.Model.Boilerplate;

namespace ECollegeAPI.Services.Courses
{
    public class FetchMyCoursesService : BaseService
    {
        public List<Course> Result { get; set; }

        public FetchMyCoursesService()
            : base()
        {
            Resource = "me/courses?expand=course";
        }

        public override void ProcessResponse(RestSharp.RestResponse resp)
        {
            var rawResult = Deserialize<List<LinkContainer<CourseLink>>>(resp, "courses");
            Result = new List<Course>();
            foreach (var linkContainer in rawResult)
            {
                if (linkContainer.Links.Count > 0)
                {
                    Result.Add(linkContainer.Links[0].Course);
                }
            }
        }
    }
}