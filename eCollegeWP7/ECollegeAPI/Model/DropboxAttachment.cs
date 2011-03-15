using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using ECollegeAPI.Model.Boilerplate;

namespace ECollegeAPI.Model
{
    public class DropboxAttachment
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string ContentUrl { get; set; }
        public User SubmissionStudent { get; set; }
        public User Author { get; set; }
        public List<AttachmentLink> Links { get; set; }
    }
}
