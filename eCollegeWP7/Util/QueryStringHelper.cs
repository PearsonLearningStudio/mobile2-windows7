using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace eCollegeWP7.Util
{
    public class QueryStringHelper
    {
        //public static Dictionary<String, String> ParseQueryString(String queryString)
        //{
        //    var result = new Dictionary<String, String>();

        //    // Source = http://increment.cx/wordpress/?p=106
        //    var regexPattern = @"\?(?<nv>(?<n>[^=]*)=(?<v>[^&]*)[&]?)*";
        //    var regex = new Regex(regexPattern, RegexOptions.ExplicitCapture);
        //    var match = regex.Match(queryString);

        //    for (var currentCapture = 0; currentCapture < match.Groups["nv"].Captures.Count; currentCapture++)
        //    {
        //        result.Add(match.Groups["n"].Captures[currentCapture].Value.ToLower(),
        //        match.Groups["v"].Captures[currentCapture].Value.ToLower());
        //    }

        //    return result;
        //}

        //Tweaked from RestSharp's source, which came from Mono

        //
        // Authors:
        //   Patrik Torstensson (Patrik.Torstensson@labs2.com)
        //   Wictor WilÃ©n (decode/encode functions) (wictor@ibizkit.se)
        //   Tim Coleman (tim@timcoleman.com)
        //   Gonzalo Paniagua Javier (gonzalo@ximian.com)

        //   Marek Habersack <mhabersack@novell.com>
        //
        // (C) 2005-2010 Novell, Inc (http://novell.com/)
        //

        //
        // Permission is hereby granted, free of charge, to any person obtaining
        // a copy of this software and associated documentation files (the
        // "Software"), to deal in the Software without restriction, including
        // without limitation the rights to use, copy, modify, merge, publish,
        // distribute, sublicense, and/or sell copies of the Software, and to
        // permit persons to whom the Software is furnished to do so, subject to
        // the following conditions:
        // 
        // The above copyright notice and this permission notice shall be
        // included in all copies or substantial portions of the Software.
        // 
        // THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
        // EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
        // MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
        // NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
        // LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
        // OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
        // WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
        //
        public static Dictionary<String, String> ParseQueryString(string query)
        {
            if (query == null)
                throw new ArgumentNullException("query");
            if (query.Length == 0 || (query.Length == 1 && query[0] == '?'))
                return new Dictionary<String, String>();
            if (query[0] == '?')
                query = query.Substring(1);

            Dictionary<String, String> result = new Dictionary<String, String>();
            ParseQueryString(query, result);
            return result;
        }

        internal static void ParseQueryString(string query, Dictionary<String, String> result)
        {
            if (query.Length == 0)
                return;

            string decoded = HttpUtility.HtmlDecode(query);
            int decodedLength = decoded.Length;
            int namePos = 0;
            bool first = true;
            while (namePos <= decodedLength)
            {
                int valuePos = -1, valueEnd = -1;
                for (int q = namePos; q < decodedLength; q++)
                {
                    if (valuePos == -1 && decoded[q] == '=')
                    {
                        valuePos = q + 1;
                    }
                    else if (decoded[q] == '&')
                    {
                        valueEnd = q;
                        break;
                    }
                }

                if (first)
                {
                    first = false;
                    if (decoded[namePos] == '?')
                        namePos++;
                }

                string name, value;
                if (valuePos == -1)
                {
                    name = null;
                    valuePos = namePos;
                }
                else
                {
                    name = HttpUtility.UrlDecode(decoded.Substring(namePos, valuePos - namePos - 1));
                }
                if (valueEnd < 0)
                {
                    namePos = -1;
                    valueEnd = decoded.Length;
                }
                else
                {
                    namePos = valueEnd + 1;
                }
                value = HttpUtility.UrlDecode(decoded.Substring(valuePos, valueEnd - valuePos));
                result.Add(name, value);
                if (namePos == -1)
                    break;
            }
        }

        
    }
}
