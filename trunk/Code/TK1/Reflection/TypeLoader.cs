using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace TK1.Reflection
{
    public class TypeLoader
    {
        public static Type Load(string typeName, string assemblyPath)
        {
            Type result = null;
            if (!string.IsNullOrEmpty(typeName) & assemblyPath != null)
            {
                Assembly assembly = AssemblyLoader.Load(assemblyPath);
                if (assembly != null)
                {
                    foreach (Type type in assembly.GetExportedTypes())
                    {
                        if (type.Name.EndsWith(typeName))
                        {
                            result = type;
                            break;
                        }
                    }
                }
            }
            return result;
        }
    }
}
