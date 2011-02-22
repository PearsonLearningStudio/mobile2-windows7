using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using ECollegeAPI.Model.Boilerplate;

namespace ECollegeAPI.Model
{
    public class DropboxAttachment
    {
        public long ID { get; set; }
        public string name;
        public string ContentUrl;
        public User SubmissionStudent { get; set; }
        public User Author { get; set; }
        public List<AttachmentLink> Links { get; set; }
    }
}
