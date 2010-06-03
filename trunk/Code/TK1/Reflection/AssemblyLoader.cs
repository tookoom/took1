using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace TK1.Reflection
{
    public class AssemblyLoader
    {
        public static Assembly Load(string path)
        {
            Assembly result = null;

            if (!string.IsNullOrEmpty(path))
            {
                try
                {
                    result = Assembly.LoadFrom(path);
                }
                catch (Exception exception) { }
                //using (FileStream fileStream = File.Open(path, FileMode.Open, FileAccess.Read))
                //{
                //    using (MemoryStream memoryStream = new MemoryStream())
                //    {
                //        byte[] buffer = new byte[1024];
                //        int read = 0;
                //        while ((read = fileStream.Read(buffer, 0, 1024)) > 0)
                //            memoryStream.Write(buffer, 0, read);
                //        result = Assembly.Load(memoryStream.ToArray());
                //    }
                //}
            }
            return result;
        }
        public static bool Save(string path, Assembly assembly)
        {
            bool result = false;

            if (!string.IsNullOrEmpty(path))
            {
                using (FileStream fileStream = File.Open(path, FileMode.Create, FileAccess.Write))
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        //byte[] buffer = new byte[1024];
                        //int read = 0;
                        //while ((read = fileStream.Read(buffer, 0, 1024)) > 0)
                        //    memoryStream.Write(buffer, 0, read);
                        //result = Assembly.Load(memoryStream.ToArray());
                    }

                }
            }
            return result;
        }
    }
}
