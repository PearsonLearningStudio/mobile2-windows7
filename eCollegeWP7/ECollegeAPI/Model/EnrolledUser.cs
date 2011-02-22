using System.Runtime.Serialization;
using System;

namespace ECollegeAPI.Model
{

    public class EnrolledUser
    {
        public long ID { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
