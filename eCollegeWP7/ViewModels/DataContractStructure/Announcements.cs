using System.Collections.Generic;
using System.Runtime.Serialization;
using System;
using System.Net;

namespace eCollegeWP7.ViewModels.DataContract
{
    [DataContract]
    public class AnnouncementList
    {
        public AnnouncementList()
        {
                AnnouncementInfoList = new List<AnnouncementInfo>();
        }

        [DataMember(Name = "announcements")]
        public List<AnnouncementInfo> AnnouncementInfoList { get; set; }
    }

    [DataContract]
    public class AnnouncementInfo
    {
        private string _ID = string.Empty;
        private string _Subject = string.Empty;
        private string _Text = string.Empty;
        private string _Submitter = string.Empty;
        private DateTime _StartDisplayDate = new DateTime(1900,1,1);
        private DateTime _EndDisplayDate = new DateTime(1900, 1, 1);

        [DataMember(Name = "id")]
        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        [DataMember(Name = "subject")]
        public string Subject
        {
            get { return _Subject; }
            set { _Subject = HttpUtility.HtmlDecode(value); }
        }

        [DataMember(Name = "text")]
        public string Text
        {
            get { return _Text; }
            set { _Text = HttpUtility.HtmlDecode(value); }
        }

        [DataMember(Name = "submitter")]
        public string Submitter
        {
            get { return _Submitter; }
            set { _Submitter = value; }
        }

        [DataMember(Name = "startDisplayDate")]
        public DateTime StartDisplayDate
        {
            get { return _StartDisplayDate; }
            set { _StartDisplayDate = value; }
        }

        [DataMember(Name = "endDisplayDate")]
        public DateTime EndDisplayDate
        {
            get { return _EndDisplayDate; }
            set { _EndDisplayDate = value; }
        }
    }
}
