using System.Runtime.Serialization;
using System.Collections.Generic;
using System;
using System.Net;

namespace eCollegeWP7.ViewModels.DataContract
{
    [DataContract]
    public class GradebookItemContainer
    {
        [DataMember(Name = "gradebookItems")]
        public List<GradebookItemInfo> GradebookItemList { get; set; }
    }

    [DataContract]
    public class GradebookItemInfo
    {
        public GradebookItemInfo()
        {
            Links = new List<GradebookItemLinkInfo>();
        }

        private string _Type;
        private string _ID;
        private string _Title;
        private double _PointsPossible;

        [DataMember(Name = "type")]
        public string Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        [DataMember(Name = "id")]
        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        [DataMember(Name = "title")]
        public string Title
        {
            get { return _Title; }
            set { _Title = HttpUtility.HtmlDecode(value); }
        }

        [DataMember(Name = "pointsPossible")]
        public double PointsPossible
        {
            get { return _PointsPossible; }
            set { _PointsPossible = value; }
        }

        [DataMember(Name = "links")]
        public List<GradebookItemLinkInfo> Links { get; set; }
    }

    [DataContract]
    public class GradebookItemLinkInfo
    {
        private string _Href;
        private string _Rel;

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
    public class GradeContainer
    {
        public GradeContainer()
        {
            Grades = new List<GradeInfo>();
        }

        [DataMember(Name = "grades")]
        public List<GradeInfo> Grades { get; set; }
    }

    [DataContract]
    public class GradeInfo
    {
        public GradeInfo()
        {
            GradedStudent = new GradedStudentInfo();
        }

        private string _ID;
        private double? _Points;
        private string _LetterGrade;
        private string _Comments;
        private DateTime _UpdatedDate;

        [DataMember(Name = "id")]
        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        [DataMember(Name = "points")]
        public double? Points
        {
            get { return _Points; }
            set { _Points = value; }
        }

        [DataMember(Name = "letterGrade")]
        public string LetterGrade
        {
            get { return _LetterGrade; }
            set { _LetterGrade = HttpUtility.HtmlDecode(value); }
        }


        [DataMember(Name = "comments")]
        public string Comments
        {
            get { return _Comments; }
            set { _Comments = HttpUtility.HtmlDecode(value); }
        }


        [DataMember(Name = "updatedDate")]
        public DateTime UpdatedDate
        {
            get { return _UpdatedDate; }
            set { _UpdatedDate = value; }
        }

        [DataMember(Name = "gradedStudent")]
        public GradedStudentInfo GradedStudent { get; set; }
    }

    [DataContract]
    public class GradedStudentInfo
    {
        public GradedStudentInfo()
        {
            Links = new List<GradedStudentLink>();
        }

        private string _ID;

        [DataMember(Name = "id")]
        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        [DataMember(Name = "links")]
        public List<GradedStudentLink> Links { get; set; }
    }

    [DataContract]
    public class GradedStudentLink
    {
        private string _Href;
        private string _Rel;

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
}
