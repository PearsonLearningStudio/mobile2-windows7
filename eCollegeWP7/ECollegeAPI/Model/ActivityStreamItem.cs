using System.Runtime.Serialization;
using System;
using eCollegeWP7.ECollegeAPI.Model;

namespace ECollegeAPI.Model
{

    public class ActivityStreamItem
    {
        public string ID { get; set; }
	    public DateTime PostedTime { get; set; }
	    public ActivityStreamActor Actor { get; set; }
	    public string Verb { get; set; }
	    public ActivityStreamObject Object { get; set; }
	    public ActivityStreamTarget Target { get; set; }
    }
}
