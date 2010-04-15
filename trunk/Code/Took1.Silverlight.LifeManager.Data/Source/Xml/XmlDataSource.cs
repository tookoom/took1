using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using Took1.Silverlight.Diagnostics;
using Took1.Silverlight.Xml;
using Took1.Silverlight.LifeManager.Data.Model;
using System.Xml;

namespace Took1.Silverlight.LifeManager.Data.Source.Xml
{
    public class XmlDataSource
    {
        DateTime creationTimeStamp = DateTime.MinValue;
        ErrorManager errorManager = new ErrorManager();


        public DataSource DataSource { get; set; }
        public string XmlContent { get; set; }

        public XmlDataSource()
        {
            DataSource = null;
        }
        
        public void Load(string xmlContent)
        {
            try
            {
                XmlContent = xmlContent;
                DataSource = XmlSerializer<DataSource>.Load(xmlContent);

                while (DataSource.AccountEntities.Contains(null))
                    DataSource.AccountEntities.Remove(null);
                while (DataSource.CategoryEntities.Contains(null))
                    DataSource.CategoryEntities.Remove(null);
                while (DataSource.EventEntities.Contains(null))
                    DataSource.EventEntities.Remove(null);
                while (DataSource.TransactionEntities.Contains(null))
                    DataSource.TransactionEntities.Remove(null);
            }
            catch (Exception exception)
            {
                errorManager.SetError("", exception);
            }
        }
        public string GenerateXml()
        {
            string result = string.Empty;

            if (DataSource.Info.Name == null)
                DataSource.Info.Name = "Took1.Silverlight.LifeManager";
            if (DataSource.Info.CreationTimeStamp == DateTime.MinValue)
                DataSource.Info.CreationTimeStamp = DateTime.Now;


            try
            {
                DataSource clonedDataSource = cloneDataSource();
                clonedDataSource.CurrentValue = null;

                result = XmlSerializer<DataSource>.Save(clonedDataSource);
                XmlContent = result;
            }
            catch (Exception exception)
            {
                errorManager.SetError("XmlDataSource.GenerateXml", exception);
            }
            return result;
        }

        private DataSource cloneDataSource()
        {
            DataSource clonedDataSource = ObjectCloning.Clone<DataSource>(DataSource);
            foreach (Account account in clonedDataSource.AccountEntities)
            {
                account.Transactions.Clear();
                account.DataSource = null;
            }
            foreach (Category category in clonedDataSource.CategoryEntities)
            {
                category.Transactions.Clear();
                category.DataSource = null;
            }
            foreach (CheckPoint checkPoint in clonedDataSource.CheckPointEntities)
            {
                checkPoint.Accounts.Clear();
                checkPoint.DataSource = null;
            }
            foreach (CheckPointAccount checkPointAccount in clonedDataSource.CheckPointAccountEntities)
            {
                checkPointAccount.Account = null;
                checkPointAccount.CheckPoint = null;
                checkPointAccount.DataSource = null;
            }
            foreach (Event ev in clonedDataSource.EventEntities)
            {
                ev.Transactions.Clear();
                ev.DataSource = null;
            }
            foreach (Transaction transaction in clonedDataSource.TransactionEntities)
            {
                transaction.Accounts.Clear();
                transaction.Events.Clear();
                transaction.TransactionAccounts.Clear();
                transaction.DataSource = null;
            }
            foreach (TransactionAccount transactionAccount in clonedDataSource.TransactionAccountEntities)
            {
                transactionAccount.Account = null;
                transactionAccount.Transaction = null;
                transactionAccount.DataSource = null;
            }
            foreach (TransactionEvent transactionEvent in clonedDataSource.TransactionEventEntities)
            {
                transactionEvent.Event = null;
                transactionEvent.Transaction = null;
                transactionEvent.DataSource = null;
            }
            foreach (SeqGenerator seqGenerator in clonedDataSource.SeqGeneratorEntities)
            {
                seqGenerator.DataSource = null;
            }
            return clonedDataSource;
        }

    }
}
