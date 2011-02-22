using System.Runtime.Serialization;

namespace ECollegeAPI.Model
{
    [DataContract]
    public class MeResult
    {
        public User Me { get; set; }
    }

    public class User
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string ClientString { get; set; }
    }
}
