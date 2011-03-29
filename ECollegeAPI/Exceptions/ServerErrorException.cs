using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using RestSharp;

namespace ECollegeAPI.Exceptions
{
    public class ServerErrorException : ServiceException
    {
        public RestResponse Response { get; set; }

        public ServerErrorException(RestResponse response)
            : base("The server returned an error")
        {
            this.Response = response;
        }
        public ServerErrorException(RestResponse response, string message)
            : base(message)
        {
            this.Response = response;
        }
        public ServerErrorException(RestResponse response, string message, Exception innerException)
            : base(message,innerException)
        {
            this.Response = response;
        }
    }
}
