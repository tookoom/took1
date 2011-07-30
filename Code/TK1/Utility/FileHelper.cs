using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TK1.Utility
{
    public class FileHelper
    {
        public static List<string> GetFiles(string rootDirectory, string fileFilter)
        {
            List<string> result = new List<string>();
            if (!string.IsNullOrEmpty(rootDirectory) & !string.IsNullOrEmpty(fileFilter))
            {
                if (Directory.Exists(rootDirectory))
                {
                    foreach (var item in Directory.GetFiles(rootDirectory, fileFilter))
                    {
                        result.Add(item);
                    }
                }
            }
            return result;

        }
    }
}
