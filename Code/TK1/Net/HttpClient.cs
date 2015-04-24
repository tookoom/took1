using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace TK1.Net
{
    public class HttpClient
    {
        public static string GetContent(string url)
        {
            string result = string.Empty;
            StringBuilder builder = new StringBuilder();

            // used on each read operation
            byte[] buffer = new byte[8192];

            // prepare the web page we will be asking for
            HttpWebRequest request = (HttpWebRequest)
                WebRequest.Create(url);

            // execute the request
            HttpWebResponse response = (HttpWebResponse)
                request.GetResponse();

            // we will read data via the response stream
            Stream responseStream = response.GetResponseStream();

            string tempString = null;
            int count = 0;

            do
            {
                // fill the buffer with data
                count = responseStream.Read(buffer, 0, buffer.Length);

                // make sure we read some data
                if (count != 0)
                {
                    // translate from bytes to ASCII text
                    tempString = Encoding.UTF8.GetString(buffer, 0, count);
                    //if (tempString.Contains("<meta"))
                    //{
                    //    if(tempString.Contains("charset="))
                    //    {

                    //    }
                    //}

                    // continue building the string
                    builder.Append(tempString);
                }
            }
            while (count > 0); // any more data to read?

            // print out page source
            result = builder.ToString();

            return result;

        }

        public static bool ValidateUrl(string url)
        {
            //using (var client = new MyClient())
            //{
            //    client.HeadOnly = true;
            //    // fine, no content downloaded
            //    string s1 = client.DownloadString("http://google.com");
            //    // throws 404
            //    string s2 = client.DownloadString("http://google.com/silly");
            //}
            return true;
        }
    }
}
