using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Took1.Silverlight.Diagnostics;
using Took1.Silverlight.LifeManager.Data.Source;
using Took1.Silverlight.LifeManager.Data.Model;
using System.Collections;

namespace Took1.Silverlight.LifeManager.Data.Controller
{
    public class BaseController
    {
        protected DataSource dataSource;
        protected ErrorManager errorManager;

        public BaseController() 
        {
            errorManager = new ErrorManager();
        }

        protected object Add(object entity, string entityName)
        {
            errorManager.Reset();
            object result = null;
            try
            {
                switch (entityName)
                {
                    case XmlDataSourceNames.Account:
                        lock (dataSource)
                        {
                            Account account = (Account)entity;
                            dataSource.AccountEntities.Add(account);
                            result = account;
                        }
                        break;
                    case XmlDataSourceNames.Category:
                        lock (dataSource)
                        {
                            Category category = (Category)entity;
                            dataSource.CategoryEntities.Add(category);
                            result = category;
                        }
                        break;
                    case XmlDataSourceNames.Event:
                        lock (dataSource)
                        {
                            Event ev = (Event)entity;
                            dataSource.EventEntities.Add(ev);
                            result = ev;
                        }
                        break;
                    case XmlDataSourceNames.Transaction:
                        lock (dataSource)
                        {
                            Transaction transaction = (Transaction)entity;
                            dataSource.TransactionEntities.Add(transaction);
                            result = transaction;
                        }
                        break;
                    case XmlDataSourceNames.SeqGenerator:
                        lock (dataSource)
                        {
                            SeqGenerator seqGenerator = (SeqGenerator)entity;
                            dataSource.SeqGeneratorEntities.Add(seqGenerator);
                            result = seqGenerator;
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception exception)
            {
                errorManager.SetError(entityName + ".Add", exception);
                errorManager.LogError();
                result = null;
            }
            return result;

        }
        protected object Get(string entityName)
        {
            object result = null;
            try
            {
                switch (entityName)
                {
                    case XmlDataSourceNames.Account:
                        lock (dataSource)
                        {
                            result = dataSource.AccountEntities;
                        }
                        break;
                    case XmlDataSourceNames.Category:
                        lock (dataSource)
                        {
                            result = dataSource.CategoryEntities;
                        }
                        break;
                    case XmlDataSourceNames.Event:
                        lock (dataSource)
                        {
                            result = dataSource.EventEntities;
                        }
                        break;
                    case XmlDataSourceNames.Transaction:
                        lock (dataSource)
                        {
                            result = dataSource.TransactionEntities;
                        }
                        break;
                    case XmlDataSourceNames.SeqGenerator:
                        lock (dataSource)
                        {
                            result = dataSource.SeqGeneratorEntities;
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception exception)
            {
                errorManager.SetError(entityName + ".Get", exception);
                errorManager.LogError();
                result = null;
            }
            return result;
        }
        protected object Get(string entityName, int ID)
        {
            object result = null;
            try
            {
                switch (entityName)
                {
                    case XmlDataSourceNames.Account:
                        lock (dataSource)
                        {
                            result = (from el in dataSource.AccountEntities
                                      where el.AccountID == ID
                                      select el).FirstOrDefault();
                        }
                        break;
                    case XmlDataSourceNames.Category:
                        lock (dataSource)
                        {
                            result = (from el in dataSource.CategoryEntities
                                      where el.CategoryID == ID
                                      select el).FirstOrDefault();
                        }
                        break;
                    case XmlDataSourceNames.CheckPoint:
                        lock (dataSource)
                        {
                            result = (from el in dataSource.CheckPointEntities
                                      where el.CheckPointID == ID
                                      select el).FirstOrDefault();
                        }
                        break;
                    case XmlDataSourceNames.Event:
                        lock (dataSource)
                        {
                            result = (from el in dataSource.EventEntities
                                      where el.EventID == ID
                                      select el).FirstOrDefault();
                        }
                        break;
                    case XmlDataSourceNames.Transaction:
                        lock (dataSource)
                        {
                            result = (from el in dataSource.TransactionEntities
                                      where el.TransactionID == ID
                                      select el).FirstOrDefault();
                        }
                        break;
                    case XmlDataSourceNames.SeqGenerator:
                        lock (dataSource)
                        {
                            result = (from el in dataSource.SeqGeneratorEntities
                                      where el.SeqGeneratorID == ID
                                      select el).FirstOrDefault();
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception exception)
            {
                errorManager.SetError(entityName + ".Get(ID)", exception);
                errorManager.LogError();
                result = null;
            }
            return result;
        }

        public bool SaveChanges()
        {
            errorManager.Reset();
            bool result;
            try
            {
                result = true;
            }
            catch (Exception exception)
            {
                errorManager.SetError("BaseController.SaveChanges", exception);
                errorManager.LogError();
                result = false;
            }
            return result;
        }
    }
}
