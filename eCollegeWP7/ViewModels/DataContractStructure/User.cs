using System.Runtime.Serialization;

namespace eCollegeWP7.ViewModels.DataContract
{
    [DataContract]
    public class UserContainer
    {
        public UserContainer()
        {
            User = new User();
        }

        [DataMember(Name = "me")]
        public User User { get; set; }
    }

    [DataContract]
    public class User
    {
        private string _ID = string.Empty;
        private string _UserName = string.Empty;
        private string _FirstName = string.Empty;
        private string _LastName = string.Empty;
        private string _eMailAdress = string.Empty;
        private string _Password = string.Empty;
        private string _Domain = string.Empty;
        private string _EpId = string.Empty;

        [DataMember(Name = "id")]
        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        [DataMember(Name = "userName")]
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        [DataMember(Name = "firstName")]
        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }

        [DataMember(Name = "lastName")]
        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }

        [DataMember(Name = "emailAddress")]
        public string eMailAdress
        {
            get { return _eMailAdress; }
            set { _eMailAdress = value; }
        }

        [DataMember(Name = "password")]
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        [DataMember(Name = "domain")]
        public string Domain
        {
            get { return _Domain; }
            set { _Domain = value; }
        }

        [DataMember(Name = "epid")]
        public string EpId
        {
            get { return _EpId; }
            set { _EpId = value; }
        }
    }
}
