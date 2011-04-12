using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace ECollegeAPI.Util
{
    public class HashUtil
    {
        public static string ToSHA1(params string[] inputs)
        {
            SHA1 hash = new SHA1Managed();

            var buffer = new StringBuilder();

            foreach (var s in inputs)
            {
                buffer.Append(s);
            }

            var rawRes = hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(buffer.ToString()));
            var result = System.BitConverter.ToString(rawRes);

            return result.Replace("-","");
        }
    }
}
