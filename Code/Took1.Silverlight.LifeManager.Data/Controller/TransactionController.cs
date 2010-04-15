using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Took1.Silverlight.LifeManager.Data.Source;
using Took1.Silverlight.LifeManager.Data.Model;
using Took1.Silverlight.LifeManager.Data.Collection;

namespace Took1.Silverlight.LifeManager.Data.Controller
{
    public class TransactionController : BaseController
    {
        /// <summary>
        /// Indica se houve erro na ultima operação
        /// </summary>
        public bool HasError
        {
            //read property
            get { return errorManager.HasError; }
        }
        /// <summary>
        /// Retorna mensagem de erro da ultima operação, se houver
        /// </summary>
        public string ErrorMessage
        {
            //read property
            get { return errorManager.ErrorMessage; }
        }

        public TransactionController(DataSource dataSource)
        {
            base.dataSource = dataSource;
        }

        public Transaction Add(Transaction transaction)
        {
            Transaction result = (Transaction)base.Add(transaction, XmlDataSourceNames.Transaction);
            return result;
        }
        public Transaction Add(string name, int categoryID, DateTime entryTimeStamp, int type)
        {
            Transaction transaction = new Transaction()
            {
                Name = name,
                CategoryID = categoryID,
                EntryTimeStamp = entryTimeStamp,
                Type = type
            };
            return Add(transaction);
        }
        public void Delete(Transaction transaction)
        {
            if (transaction != null)
            {
                if (dataSource.TransactionEntities.Contains(transaction))
                    dataSource.TransactionEntities.Remove(transaction);
            }
        }
        public void Delete(int transactionID)
        {
            Delete(Get(transactionID));
        }
        public List<Transaction> Get()
        {
            return (List<Transaction>)base.Get(XmlDataSourceNames.Transaction);
        }
        public List<Transaction> Get(TransactionTypes transactionType)
        {
            return ((TransactionCollection)base.Get(XmlDataSourceNames.Transaction)).Where(obj => obj.Type == (int)transactionType).ToList();
        }

        public Transaction Get(int transactionID)
        { 
            return (Transaction)base.Get(XmlDataSourceNames.Transaction,transactionID);
        }
        public void Update(Transaction transaction)
        {
        }

    }
}
