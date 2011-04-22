using System.Collections.Generic;
using System.Runtime.Serialization;
using System;
using eCollegeWP7.ECollegeAPI.Model;
using ECollegeAPI.Model.Boilerplate;
using System.Text.RegularExpressions;

namespace ECollegeAPI.Model
{
    public enum UpcomingEventType
    {
        QuizExamTest,
        Thread,
        Html,
        Ignored
    }

    public enum CategoryType
    {
        Start,
        End,
        Due
    }

    public class UpcomingEventItem
    {
        public WhenWrapper When { get; set; }
        public long ID { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public List<Link> Links { get; set; }

        private long _courseId = -1;
        public long CourseID
        {
            get
            {
                if (_courseId == -1) {
                    foreach (var link in Links)
                    {
                        Match match = Regex.Match(link.Href, @"courses/([^""\/]*)", RegexOptions.IgnoreCase);
                        if (match.Success)
                        {
                            string courseId = match.Groups[1].Value;
                            _courseId = Int64.Parse(courseId);
                        }
                    }
                }
                return _courseId;
            }
        }

        public long ThreadID
        {
            get { return ID; }
        }

        public long MultimediaID
        {
            get { return ID; }
        }

        public CategoryType CategoryType
        {
            get
            {
                if ("start".Equals(Category)) return CategoryType.Start;
                if ("end".Equals(Category)) return CategoryType.End;
                return CategoryType.Due;
            }
        }

        public UpcomingEventType EventType
        {
            get
            {
                if (Regex.Match(Type,@"^(HTML|MANAGED_OD|MANAGED_HTML)$",RegexOptions.IgnoreCase).Success)
                {
                    return UpcomingEventType.Html;
                }

                if (Regex.Match(Type, @"^(THREAD|MANAGED_THREADS)$", RegexOptions.IgnoreCase).Success)
                {
                    return UpcomingEventType.Thread;
                }

                if (Regex.Match(Type, @"^(IQT)$", RegexOptions.IgnoreCase).Success)
                {
                    return UpcomingEventType.QuizExamTest;
                }
                return UpcomingEventType.Ignored;
            }
        }
    }
}
