﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace ECollegeAPI.Model
{
    public class DiscussionResponse
    {
        public long ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public User Author { get; set; }
        public DateTime PostedDate { get; set; }
    }
}
