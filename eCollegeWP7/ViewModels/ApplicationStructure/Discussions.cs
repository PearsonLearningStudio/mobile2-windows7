using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace eCollegeWP7.ViewModels.Application
{
    public class ThreadedDiscussion
    {
        public ThreadedDiscussion()
        {
            Topics = new List<Topic>();
        }

        public string ID { get; set; }
        public string IntroductoryText { get; set; }
        public string Href { get; set; }
        public string MeHref { get; set; }

        public List<Topic> Topics { get; set; }
    }

    public class Topic
    {
        public Topic()
        {
            ResponseCounts = new Counts();
            Responses = new List<Response>();
        }

        public string ID { get; set; }
        public string Title { get; set; }
        public string ItemTitle { get; set; }
        public string Description { get; set; }
        public string Href { get; set; }
        public string MeHref { get; set; }
        public Counts ResponseCounts { get; set; }
        public string ImageUrl { get { return @"Images\discussion.png"; } }
        public List<Response> Responses { get; set; }
    }

    public class Response
    {
        public Response()
        {
            ResponseAuthor = new Author();
            ResponseCounts = new Counts();
        }

        public string ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Href { get; set; }
        public string MeHref { get; set; }
        public string ParentID { get; set; }
        public Author ResponseAuthor { get; set; }
        public DateTime PostedDate { get; set; }
        public string FormatedPostedDate { get; set; }
        public bool IsTopicResponse { get; set; }
        public bool MarkedAsRead { get; set; }
        public string ReadStatusImage { get; set; }
        public Counts ResponseCounts { get; set; }
        public bool ReadStatusLoaded { get; set; }
        public string ImageUrl { get { return @"Images\discussion.png"; } }
    }

    public class Author
    {
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string eMail { get; set; }
        public string Href { get; set; }
        public bool IsLoaded { get; set; }
    }

    public class Counts
    {
        public int TotalResponseCount { get; set; }
        public int PersonalResponseCount { get; set; }
        public int LastDayResponseCount { get; set; }
        public int Last24HourResponseCount { get; set; }
        public string ResponsesBoxColor { get; set; }
        public bool IsLoaded { get; set; }

        private int _UnreadResponseCount = 0;
        public int UnreadResponseCount
        {
            get { return _UnreadResponseCount; }
            set
            {
                if (value == 0)
                    ResponsesBoxColor = Colors.Transparent.ToString();
                else
                    ResponsesBoxColor = "#FFFEF1AB";

                _UnreadResponseCount = value;
            }
        }
    }
}
