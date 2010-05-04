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

using System.Xml.Serialization;
using System.IO;
using System.Xml.Linq;

namespace Took1.Silverlight.Xml
{
    public class XmlSerializer<T> where T : class
    {
        /// <summary>
        /// Loads an object from an XML file in Document format.
        /// </summary>
        /// <example>
        /// <code>
        /// serializableObject = ObjectXMLSerializer&lt;SerializableObject&gt;.Load(@"C:\XMLObjects.xml");
        /// </code>
        /// </example>
        /// <param name="path">Path of the file to load the object from.</param>
        /// <returns>Object loaded from an XML file in Document format.</returns>
        public static T Load(string fileContent)
        {
            T serializableObject = loadFromXml(fileContent, null);
            return serializableObject;
        }
        /// <summary>
        /// Loads an object from an XML file in Document format, supplying extra data types to enable deserialization of custom types within the object.
        /// </summary>
        /// <example>
        /// <code>
        /// serializableObject = ObjectXMLSerializer&lt;SerializableObject&gt;.Load(@"C:\XMLObjects.xml", new Type[] { typeof(MyCustomType) });
        /// </code>
        /// </example>
        /// <param name="path">Path of the file to load the object from.</param>
        /// <param name="extraTypes">Extra data types to enable deserialization of custom types within the object.</param>
        /// <returns>Object loaded from an XML file in Document format.</returns>
        public static T Load(string fileContent, System.Type[] extraTypes)
        {
            T serializableObject = loadFromXml(fileContent, extraTypes);
            return serializableObject;
        }
        /// <summary>
        /// Loads an object from an XML file in Document format.
        /// </summary>
        /// <example>
        /// <code>
        /// serializableObject = ObjectXMLSerializer&lt;SerializableObject&gt;.Load(@"C:\XMLObjects.xml");
        /// </code>
        /// </example>
        /// <param name="path">Path of the file to load the object from.</param>
        /// <returns>Object loaded from an XML file in Document format.</returns>
        public static T Load(XElement root)
        {
            string fileContent = root.ToString();
            T serializableObject = loadFromXml(fileContent, null);
            return serializableObject;
        }

        /// <summary>
        /// Saves an object to an XML file in Document format.
        /// </summary>
        /// <example>
        /// <code>        
        /// SerializableObject serializableObject = new SerializableObject();
        /// 
        /// ObjectXMLSerializer&lt;SerializableObject&gt;.Save(serializableObject, @"C:\XMLObjects.xml");
        /// </code>
        /// </example>
        /// <param name="serializableObject">Serializable object to be saved to file.</param>
        /// <param name="path">Path of the file to save the object to.</param>
        public static string Save(T serializableObject)
        {
            return SaveToXml(serializableObject, null);
        }
        /// <summary>
        /// Saves an object to an XML file in Document format, supplying extra data types to enable serialization of custom types within the object.
        /// </summary>
        /// <example>
        /// <code>        
        /// SerializableObject serializableObject = new SerializableObject();
        /// 
        /// ObjectXMLSerializer&lt;SerializableObject&gt;.Save(serializableObject, @"C:\XMLObjects.xml", new Type[] { typeof(MyCustomType) });
        /// </code>
        /// </example>
        /// <param name="serializableObject">Serializable object to be saved to file.</param>
        /// <param name="path">Path of the file to save the object to.</param>
        /// <param name="extraTypes">Extra data types to enable serialization of custom types within the object.</param>
        public static string Save(T serializableObject, System.Type[] extraTypes)
        {
            return SaveToXml(serializableObject, extraTypes);
        }
        public static XElement SaveToXElement(T serializableObject)
        {
            XElement root = null;
            string xmlContent = SaveToXml(serializableObject, null);
            root = XElement.Load(new StringReader(xmlContent));
            return root;
        }



        private static T loadFromXml(string fileContent, System.Type[] extraTypes)
        {
            
            T serializableObject = null;

            using (StringReader stringReader = new StringReader(fileContent))
            {
                try
                {
                    XmlSerializer xmlSerializer = createXmlSerializer(extraTypes);
                    serializableObject = xmlSerializer.Deserialize(stringReader) as T;
                }
                catch (Exception exception) { }
            }

            return serializableObject;
        }
        private static XmlSerializer createXmlSerializer(System.Type[] extraTypes)
        {
            Type type = typeof(T);

            XmlSerializer xmlSerializer = null;

            if (extraTypes != null)
                xmlSerializer = new XmlSerializer(type, extraTypes);
            else
                xmlSerializer = new XmlSerializer(type);

            return xmlSerializer;
        }
        private static string SaveToXml(T serializableObject, System.Type[] extraTypes)
        {
            string result = string.Empty;
            using (StringWriter stringWriter = new StringWriter())
            {
                XmlSerializer xmlSerializer = createXmlSerializer(extraTypes);
                xmlSerializer.Serialize(stringWriter, serializableObject);
                result = stringWriter.ToString();
            }
            return result;
        }

    }
}
