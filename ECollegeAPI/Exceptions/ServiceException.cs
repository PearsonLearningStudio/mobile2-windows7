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
    public class ServiceException : Exception
    {

        private bool _IsHandled = false;
        public bool IsHandled
        {
            get { return _IsHandled; }
            set { _IsHandled = value; }
        }

        public ServiceException(string message) : base(message)
        {
        }

        public ServiceException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
