using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Took1.Collection
{
	/// <summary>
	/// Par de strings Chave/Valor
	/// </summary>
    public class StringPair
    {
        #region PRIVATE MEMBERS
        /// <summary>
        /// Chave
        /// </summary>
        string key;
        /// <summary>
        /// Valor
        /// </summary>
        string value;
        
        #endregion

        #region PUBLIC PROPERTIES
        /// <summary>
        /// Chave
        /// </summary>
        public string Key
        {
            get { return key; }
            set { key = value; }
        }
        /// <summary>
        /// Valor
        /// </summary>
        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        #endregion
    }
}
