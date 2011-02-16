using System.Collections.Generic;
using System.Runtime.Serialization;
using System;
using System.Net;

namespace eCollegeWP7.ViewModels.DataContract
{
    [DataContract]
    public class DropboxBasketList
    {
        public DropboxBasketList()
        {
            DropBoxBasketInfoList = new List<DropboxBasketInfo>();
        }

        [DataMember(Name = "dropboxBaskets")]
        public List<DropboxBasketInfo> DropBoxBasketInfoList { get; set; }
    }

    [DataContract]
    public class DropboxBasketInfo
    {
        public DropboxBasketInfo()
        {
            LinkInfos = new List<DropboxInfoLink>();
        }

        private string _ID;
        private string _Title;

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

        [DataMember(Name = "links")]
        public List<DropboxInfoLink> LinkInfos { get; set; }
    }

    [DataContract]
    public class DropboxInfoLink
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
    public class DropboxBasketMessageContainer
    {
        public DropboxBasketMessageContainer()
        {
            Messages = new List<DropBoxBasketMessageInfo>();
        }

        [DataMember(Name = "messages")]
        public List<DropBoxBasketMessageInfo> Messages { get; set; }
    }

    [DataContract]
    public class DropBoxBasketMessageInfo
    {
        public DropBoxBasketMessageInfo()
        {
            Student = new DropboxMessageUserInfo();
            Author = new DropboxMessageUserInfo();
        }

        private string _ID;
        private DateTime _Date;
        private string _Comments;
  
        [DataMember(Name = "id")]
        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        [DataMember(Name = "date")]
        public DateTime Date
        {
            get { return _Date; }
            set { _Date = value; }
        }

        [DataMember(Name = "comments")]
        public string Comments
        {
            get { return _Comments; }
            set { _Comments = HttpUtility.HtmlDecode(value); }
        }

        [DataMember(Name = "submissionStudent")]
        public DropboxMessageUserInfo Student { get; set; }

        [DataMember(Name = "author")]
        public DropboxMessageUserInfo Author { get; set; }
    }

    [DataContract]
    public class DropboxMessageUserInfo
    {
        public DropboxMessageUserInfo()
        {
            Links = new List<SubmissionStudentLink>();
        }

        private string _ID = string.Empty;
        private string _FirstName = string.Empty;
        private string _LastName = string.Empty;
        private string _eMailAdress = string.Empty;

        [DataMember(Name = "id")]
        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
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

        [DataMember(Name = "email")]
        public string eMailAdress
        {
            get { return _eMailAdress; }
            set { _eMailAdress = value; }
        }

        [DataMember(Name = "links")]
        public List<SubmissionStudentLink> Links { get; set; }
    }

    [DataContract]
    public class SubmissionStudentLink
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
}
