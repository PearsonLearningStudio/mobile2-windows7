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
using ECollegeAPI.Services;

namespace ECollegeAPI.Exceptions
{
    public class ClientErrorException : ServiceException
    {
        public BaseService Service { get; set; }

        public ClientErrorException(BaseService service)
            : base("Client error while processing request")
        {
            this.Service = service;
        }
        public ClientErrorException(BaseService service, Exception innerException)
            : base("Client error while processing request", innerException)
        {
            this.Service = service;
        }
        public ClientErrorException(BaseService service, string message, Exception innerException)
            : base(message, innerException)
        {
            this.Service = service;
        }
    }
}
