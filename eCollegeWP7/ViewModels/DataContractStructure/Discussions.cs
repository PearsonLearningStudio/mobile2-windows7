using System.Runtime.Serialization;
using System.Collections.Generic;
using System;
using System.Windows.Media;
using System.Net;

namespace eCollegeWP7.ViewModels.DataContract
{
    [DataContract]
    public class DiscussionIds
    {
        public DiscussionIds()
        {
            DiscussionIdList = new List<DiscussionId>();
        }

        [DataMember(Name = "threadedDiscussions")]
        public List<DiscussionId> DiscussionIdList { get; set; }
    }

    [DataContract]
    public class DiscussionId
    {
        private string _ID = string.Empty;

        [DataMember(Name = "id")]
        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }


        [DataMember(Name = "introductoryText", IsRequired = false)]
        public string IntroductoryText { get; set; }
    }

    [DataContract]
    public class DiscussionTopics
    {
        public DiscussionTopics()
        {
            TopicList = new List<DiscussionTopic>();
        }

        public string CourseId { get; set; }

        [DataMember(Name = "topics")]
        public List<DiscussionTopic> TopicList { get; set; }
    }

    [DataContract]
    public class DiscussionTopic
    {
        private string _ID = string.Empty;
        private string _Title = string.Empty;
        private string _Description = string.Empty;

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

        [DataMember(Name = "description")]
        public string Description
        {
            get { return _Description; }
            set { _Description = HttpUtility.HtmlDecode(value); }
        }
    }

    [DataContract]
    public class DiscussionResponseContainer
    {
        public DiscussionResponseContainer()
        {
            ResponseCount = new DiscussionResponse();
        }

        [DataMember(Name = "responseCounts")]
        public DiscussionResponse ResponseCount { get; set; }
    }

    [DataContract]
    public class DiscussionResponse
    {
        private int _TotalResponseCount = 0;
        private int _UnreadResponseCount = 0;
        private int _PersonalResponseCount = 0;

        [DataMember(Name = "totalResponseCount")]
        public int TotalResponseCount
        {
            get { return _TotalResponseCount; }
            set { _TotalResponseCount = value; }
        }

        [DataMember(Name = "unreadResponseCount")]
        public int UnreadResponseCount
        {
            get { return _UnreadResponseCount; }
            set { _UnreadResponseCount = value; }
        }

        [DataMember(Name = "personalResponseCount")]
        public int PersonalResponseCount
        {
            get { return _PersonalResponseCount; }
            set { _PersonalResponseCount = value; }
        }
    }

    [DataContract]
    public class DiscussionResponseInfos
    {
        public DiscussionResponseInfos()
        {
            ResponseLinkList = new List<DiscussionResponseInfo>();
        }

        [DataMember(Name = "responses")]
        public List<DiscussionResponseInfo> ResponseLinkList { get; set; }
    }

    [DataContract]
    public class DiscussionResponseInfo
    {
        private string _ID = string.Empty;
        private string _Title = string.Empty;
        private string _Description = string.Empty;
        private DateTime _PostedDate = new DateTime(1);
        private AuthorInfos _AuthorLink = new AuthorInfos();

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

        [DataMember(Name = "description")]
        public string Description
        {
            get { return _Description; }
            set
            {
                value = value.Replace(Environment.NewLine, string.Empty);
                _Description = HttpUtility.HtmlDecode(value);
            }
        }

        [DataMember(Name = "postedDate")]
        public DateTime PostedDate
        {
            get { return _PostedDate; }
            set { _PostedDate = value; }

        }

        [DataMember(Name = "author")]
        public AuthorInfos AuthorLink
        {
            get { return _AuthorLink; }
            set { _AuthorLink = value; }
        }
    }

    [DataContract]
    public class AuthorInfos
    {
        public AuthorInfos()
        {
            AuthorLinkInfo = new List<AuthorInfo>();
        }

        [DataMember(Name = "links")]
        public List<AuthorInfo> AuthorLinkInfo { get; set; }
    }

    [DataContract]
    public class AuthorInfo
    {
        public AuthorInfo()
        {
            if (Info == null)
                Info = new User();
        }

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

        [DataMember(Name = "responseAuthor")]
        public User Info { get; set; }
    }

    [DataContract]
    public class ReadStatusContainer
    {
        public ReadStatusContainer()
        {
            ReadStatus = new ReadStatus();
        }

        [DataMember(Name = "readStatus")]
        public ReadStatus ReadStatus { get; set; }
    }

    [DataContract]
    public class ReadStatus
    {
        private bool _MarkedAsRead = false;

        [DataMember(Name = "markedAsRead")]
        public bool MarkedAsRead
        {
            get { return _MarkedAsRead; }
            set { _MarkedAsRead = value; }
        }
    }

    [DataContract]
    public class DoResponseContainer
    {
        public DoResponseContainer()
        {
            if (Response == null)
                Response = new DoResponse();
        }

        [DataMember(Name = "response")]
        public DoResponse Response { get; set; }

        public string Href { get; set; }
    }

    [DataContract]
    public class DoResponse
    {
        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }
    }

    [DataContract]
    public class DoMarkAsReadContainer
    {
        public DoMarkAsReadContainer()
        {
            if (Response == null)
                Response = new DoMarkAsRead();
        }

        [DataMember(Name = "readStatus")]
        public DoMarkAsRead Response { get; set; }

        public string Href { get; set; }
    }

    [DataContract]
    public class DoMarkAsRead
    {
        [DataMember(Name = "markedAsRead")]
        public bool MarkedAsRead { get; set; }
    }
}
