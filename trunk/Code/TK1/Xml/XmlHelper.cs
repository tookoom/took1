using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace TK1.Xml
{
    public class XmlHelper
    {
        public static string Normatize(string xmlContent)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(xmlContent))
            {
                if (xmlContent.Contains("\n "))
                    xmlContent = xmlContent.Replace("\n ", string.Empty);
                if (xmlContent.Contains("\n"))
                    xmlContent = xmlContent.Replace("\n", string.Empty);

                StringReader stringReader = new StringReader(xmlContent);
                var xml = XElement.Load(stringReader);
                if (xml != null)
                    result = xml.ToString();
            }
            return result;
        }
        public static string NormatizeFile(string path)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(path))
            {
                if (File.Exists(path))
                {
                    string xmlContent = File.ReadAllText(path);
                    result = Normatize(xmlContent);
                    File.Delete(path);
                    File.WriteAllText(path, result);
                }
            }
            return result;
        }

    }
}
