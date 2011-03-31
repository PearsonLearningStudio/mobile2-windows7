using System.Runtime.Serialization;
using System;

namespace ECollegeAPI.Model
{
    public class RosterUser
    {
        public long ID { get; set; }
        public string RoleType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonaId { get; set; }

        public string DisplayName
        {
            get { return FirstName + " " + LastName; }
        }

        public string LastNameFirstChar
        {
            get
            {
                if (LastName != null && LastName.Length > 0)
                {
                    return LastName.Substring(0, 1).ToUpper();
                }
                return "?";
            }
        }

        public string FriendlyRole
        {
            get
            {
                var lowerRole = RoleType == null ? null : RoleType.ToLower();
                if ("prof".Equals(lowerRole))
                {
                    return "Instructor";
                }
                if ("stud".Equals(lowerRole))
                {
                    return "Student";
                }
                return RoleType;
            }
        }
    }
}
