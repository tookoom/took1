using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using TK1.Xml;

namespace TK1.Collection
{
	/// <summary>
	/// Dicionário de pares de string (chave e valor)
	/// </summary>
    public class StringDictionary : List<StringPair>
    {
        /// <summary>
        /// Indica se existe na coleção um valor assoviado à chave indicada no parâmetro
        /// </summary>
        /// <param name="key">Chave</param>
        public bool ContainsKey(string key)
        {
            return Get(key) != null;
        }

		/// <summary>
		/// Atribui valor a uma chave especificada. Caso chave não exista no dicionário,
		/// será adicionada.
		/// </summary>
		/// <param name="key">Chave</param>
		/// <param name="value">Valor</param>
        public void Set(string key, string value)
        {
            if (Get(key) == null)
                this.Add(new StringPair() { Key = key, Value = value });
            else
            {
                var pair = (from el in this
                            where el.Key == key
                            select el).FirstOrDefault();
                if (pair != null)
                    pair.Value = value;
            }
        }
		/// <summary>
		/// Retorna string contendo o valor associado à chave ou nulo caso chave nao exista.
		/// 
		/// </summary>
		/// <param name="key">Chave a ser procurada</param>
        public string Get(string key)
        {
            string result = null;

            var pair = (from el in this
                        where el.Key == key
                        select el).FirstOrDefault();
            if (pair != null)
                result = pair.Value;

            return result;
        }

		/// <summary>
		/// Serializa dicionário e restorna string com XML equivalente
		/// </summary>
        public string Serialize()
        {
            string result = null;
            try
            {
                XElement root = XmlSerializer<StringDictionary>.SaveToXElement(this);
                if (root != null)
                    result = root.ToString();
            }
            catch (Exception)
            {

            }
            return result;
        }

        /// <summary>
        /// Transforma dicionário em string na forma [key1="value1" key2="value2"]
        /// </summary>
        public string ToHtmlAttributeString()
        {
            string result = string.Empty;
            foreach (StringPair stringPair in this)
            {
                result += string.Format(" {0}=\"{1}\"", stringPair.Key, stringPair.Value);
            }
            return result;
        }
        /// <summary>
		/// Transforma dicionário em string na forma "key1=value1;key2=value2"
		/// </summary>
        public string ToKeyValueString()
        {
            string result = string.Empty;
            foreach (StringPair stringPair in this)
            {
                result += string.Format("{0}={1};", stringPair.Key, stringPair.Value);
            }
            return result;
        }
		/// <summary>
		/// Transforma dicionário em string para uso como query string (web) na forma
		/// "key1=value1&key2=value2"
		/// </summary>
        public string ToQueryString()
        {
            string result = string.Empty;
            foreach (StringPair stringPair in this)
                result += string.Format("{0}={1}&", stringPair.Key, stringPair.Value);
            if (result != string.Empty)
            {
                result = result.Remove(result.LastIndexOf("&"));
                result = "?" + result;
            }
            return result;
        }

        /// <summary>
		/// Cria nodo XML contendo os dados de DictionaryString, usando formata?o
		/// personalizada (nomes dos elementos e atributos s?o passados por par?metros).
		/// </summary>
		/// <param name="rootName">Nome do Elemento raiz.</param>
		/// <param name="itemName">Nome de cada elemento de dados (chave + valor)</param>
		/// <param name="keyName">Nome do atributo "chave"</param>
		/// <param name="valueName">Nome do atributo "valor"</param>
        public XElement ToXElement(string rootName, string itemName, string keyName, string valueName)
        {
            XElement root = new XElement(rootName,
                from el in this
                select new XElement(itemName,
                    new XAttribute(keyName, el.Key),
                    new XAttribute(valueName, el.Value)
                    ));
            return root;
        }
        /// <summary>
		/// Cria nodo XML contendo os dados de DictionaryString, usando formata??o
		/// parcialmente personalizada (elemento raiz e de dados passado por parametros,
		/// atributo chave "Key", atributo valor "Value").
		/// </summary>
		/// <param name="rootName">Nome do Elemento raiz.</param>
		/// <param name="itemName">Nome de cada elemento de dados (chave + valor)</param>
        public XElement ToXElement(string rootName, string itemName)
        {
            return ToXElement(rootName, itemName, "Key", "Value");
        }
        /// <summary>
		/// Cria nodo XML contendo os dados de DictionaryString, usando formata??o padr?o
		/// (elemento raiz "StringDictionary", elementos de dado "KeyValuePair", atributo
		/// chave "Key", atributo valor "Value").
		/// </summary>
        public XElement ToXElement()
        {
            return ToXElement("StringDictionary", "KeyValuePair");
        }


        public static StringDictionary LoadFromQueryString(string queryString)
        {
            StringDictionary result = new StringDictionary();
            foreach (string keyValueString in queryString.Split('&'))
            {
                string[] keyValueArray = keyValueString.Split('=');
                if(keyValueArray.Count()==2)
                    result.Set(keyValueArray[0],keyValueArray[1]);
            }
            return result;

        }


    }

}
