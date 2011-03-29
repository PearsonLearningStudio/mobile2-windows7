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
    public class DeserializationException : ServiceException
    {
        public DeserializationException(Exception innerException)
            : base("There was a problem parsing the result from the server", innerException)
        {

        }
    }
}
