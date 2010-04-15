using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.IO.IsolatedStorage;
using System.Xml.Linq;

namespace Took1.Xml
{
    /// <summary>
    /// Classe utilizada com serialização de objetos.
    /// </summary>
    public class XmlSerializer<T> where T : class
    {
        public XmlSerializer() { }


        /// <summary>
        /// Carrega objeto serializado a partir de string.
        /// </summary>
        /// <example>
        /// <code>
        /// serializableObject = ObjectXMLSerializer&lt;SerializableObject&gt;.Load(content);
        /// </code>
        /// </example>
        /// <param name="fileContent">objeto serializado  na forma de string</param>
        /// <returns>Objeto do tipo especificado</returns>
        public static T Load(string fileContent)
        {
            T serializableObject = loadFromXml(fileContent, null);
            return serializableObject;
        }
        /// <summary>
        /// Carrega objeto serializado a partir de string.
        /// </summary>
        /// <example>
        /// <code>
        /// serializableObject = ObjectXMLSerializer&lt;SerializableObject&gt;.Load(content);
        /// </code>
        /// </example>
        /// <param name="fileContent">objeto serializado  na forma de string</param>
        /// <param name="extraTypes">Tipos extra de dados serializados</param>
        /// <returns>Objeto do tipo especificado</returns>
        public static T Load(string fileContent, System.Type[] extraTypes)
        {
            T serializableObject = loadFromXml(fileContent, extraTypes);
            return serializableObject;
        }
        /// <summary>
        /// Carrega objeto serializado a partir de XElement.
        /// </summary>
        /// <example>
        /// <code>
        /// serializableObject = ObjectXMLSerializer&lt;SerializableObject&gt;.Load(content);
        /// </code>
        /// </example>
        /// <param name="root">XElement contendo a raiz dos dados serializados</param>
        /// <returns>Objeto do tipo especificado</returns>
        public static T Load(XElement root)
        {
            string fileContent = root.ToString();
            T serializableObject = loadFromXml(fileContent, null);
            return serializableObject;
        }

        /// <summary>
        /// Serializa objeto.
        /// </summary>
        /// <example>
        /// <code>        
        /// SerializableObject serializableObject = new SerializableObject();
        /// 
        /// string content = ObjectXMLSerializer&lt;SerializableObject&gt;.Save(serializableObject);
        /// </code>
        /// </example>
        /// <param name="serializableObject">Objeto a serializar.</param>
        public static string Save(T serializableObject)
        {
            return saveToXml(serializableObject, null);
        }
        /// <summary>
        /// Serializa objeto.
        /// </summary>
        /// <example>
        /// <code>        
        /// SerializableObject serializableObject = new SerializableObject();
        /// 
        /// string content = ObjectXMLSerializer&lt;SerializableObject&gt;.Save(serializableObject);
        /// </code>
        /// </example>
        /// <param name="serializableObject">Objeto a serializar.</param>
        /// <param name="extraTypes">Tipos extra de dados serializados</param>
        public static string Save(T serializableObject, System.Type[] extraTypes)
        {
            return saveToXml(serializableObject, extraTypes);
        }
        /// <summary>
        /// Serializa objeto e retorna XElement
        /// </summary>
        /// <example>
        /// <code>        
        /// SerializableObject serializableObject = new SerializableObject();
        /// 
        /// XEelement root = ObjectXMLSerializer&lt;SerializableObject&gt;.SaveToXElement(serializableObject);
        /// </code>
        /// </example>
        /// <param name="serializableObject">Objeto a serializar.</param>
        public static XElement SaveToXElement(T serializableObject)
        {
            XElement root = null;
            string xmlContent = saveToXml(serializableObject, null);
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
        private static string saveToXml(T serializableObject, System.Type[] extraTypes)
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
