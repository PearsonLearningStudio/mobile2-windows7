using System.Runtime.Serialization;
using System;

namespace ECollegeAPI.Model
{
    public class ActivityStreamActor
    {
        public string Role { get; set; }
        public long ReferenceId { get; set; }
        public string Title { get; set; }
        public string ObjectType { get; set; }
    }
}
