using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;

namespace eCollegeWP7.ViewModels.DataContract
{
    [DataContract]
    public class CourseLinks
    {
        public CourseLinks()
        {
            CourseLinkList = new List<CourseLinkContainer>();
        }

        [DataMember(Name = "courses")]
        public List<CourseLinkContainer> CourseLinkList { get; set; }
    }

    [DataContract]
    public class CourseLinkContainer
    {
        private CourseLink[] _ObjLinks = new CourseLink[1];

        [DataMember(Name = "links")]
        public CourseLink[] ObjLinks
        {
            get { return _ObjLinks; }
            set
            {
                _ObjLinks = value;
                Link = ObjLinks[0];
            }
        }

        public CourseLink Link { get; set; }
    }

    [DataContract]
    public class CourseLink
    {
        private string _Href = string.Empty;
        private string _Rel = string.Empty;

        [DataMember(Name = "href")]
        public string Href
        {
            get { return _Href; }
            set { _Href = value; }
        }

        [DataMember(Name = "rel")]
        public string Rel
        {
            get { return _Rel; }
            set { _Rel = value; }
        }
    }

    [DataContract]
    public class CourseInfoContainer
    {
        public List<CourseInfo> _lstInfo = new List<CourseInfo>();

        [DataMember(Name = "courses")]
        public List<CourseInfo> LstInfo
        {
            get
            {
                return _lstInfo;
            }
            set
            {
                _lstInfo = value;
                Info = _lstInfo[0];
            }
        }

        public CourseInfo Info { get; set; }
    }

    [DataContract]
    public class CourseInfo
    {
        public CourseInfo()
        {
            InstructorLinks = new CourseUserLinkContainer();
            TeachingAssistantLinks = new CourseUserLinkContainer();
            StudentLinks = new CourseUserLinkContainer();
        }

        private string _ID = string.Empty;
        private string _DisplayCourseCode = string.Empty;
        private string _Title = string.Empty;

        [DataMember(Name = "id")]
        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        [DataMember(Name = "displayCourseCode")]
        public string DisplayCourseCode
        {
            get { return _DisplayCourseCode; }
            set { _DisplayCourseCode = HttpUtility.HtmlDecode(value); }
        }

        [DataMember(Name = "title")]
        public string Title
        {
            get { return _Title; }
            set { _Title = HttpUtility.HtmlDecode(value); }
        }

        [DataMember(Name = "callNumbers")]
        public object[] CallNumbers { get; set; }

        [DataMember(Name = "instructors")]
        public CourseUserLinkContainer InstructorLinks { get; set; }

        [DataMember(Name = "teachingAssistants")]
        public CourseUserLinkContainer TeachingAssistantLinks { get; set; }

        [DataMember(Name = "students")]
        public CourseUserLinkContainer StudentLinks { get; set; }
    }

    [DataContract]
    public class CourseUserLinkContainer
    {
        public CourseUserLinkContainer()
        {
            Instructors = new List<CourseUserLink>();
        }

        [DataMember(Name = "links")]
        public List<CourseUserLink> Instructors { get; set; }
    }

    [DataContract]
    public class CourseUserLink
    {
        private string _Href = string.Empty;
        private string _Rel = string.Empty;
        private string _Title = string.Empty;

        [DataMember(Name = "href")]
        public string Href
        {
            get { return _Href; }
            set { _Href = value; }
        }

        [DataMember(Name = "rel")]
        public string Rel
        {
            get { return _Rel; }
            set { _Rel = value; }
        }

        [DataMember(Name = "title")]
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
    }

    [DataContract]
    public class CourseUserInfoContainer
    {
        public CourseUserInfoContainer()
        {
            Users = new List<User>();
        }

        [DataMember(Name = "instructors")]
        public List<User> Users { get; set; }
    }

    [DataContract]
    public class CourseItemInfoContainer
    {
        public CourseItemInfoContainer()
        {
            Items = new List<CourseItemInfo>();
        }

        [DataMember(Name = "items")]
        public List<CourseItemInfo> Items { get; set; }
    }

    [DataContract]
    public class CourseItemInfo
    {
        private string _Title = string.Empty;

        [DataMember(Name = "title")]
        public string Title
        {
            get { return _Title; }
            set { _Title = HttpUtility.HtmlDecode(value); }
        }
    }
}
