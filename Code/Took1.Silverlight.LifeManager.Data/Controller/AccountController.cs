using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Took1.Silverlight.LifeManager.Data.Model;
using Took1.Silverlight.LifeManager.Data.Source;
using Took1.Silverlight.LifeManager.Data.Collection;

namespace Took1.Silverlight.LifeManager.Data.Controller
{
    public class AccountController : BaseController
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

        public AccountController(DataSource dataSource)
        {
            base.dataSource = dataSource;
        }

        public Account Add(Account account)
        {
            Account result = (Account)base.Add(account, XmlDataSourceNames.Account);
            return result;
        }
        public Account Add(string name, string description, DateTime creationTimeStamp)
        {
            Account account = new Account()
            {
                CreationTimeStamp = creationTimeStamp,
                Description = description,
                Name = name
            };
            return Add(account);
        }
        public void Delete(Account account)
        {
            if (account != null)
            {
                if (dataSource.AccountEntities.Contains(account))
                    dataSource.AccountEntities.Remove(account);
            }
        }
        public void Delete(int accountID)
        {
            Delete(Get(accountID));
        }
        public AccountCollection Get()
        {
            return (AccountCollection)base.Get(XmlDataSourceNames.Account);
        }
        public Account Get(int accountID)
        { 
            return (Account)base.Get(XmlDataSourceNames.Account,accountID);
        }
        public void Update(Account account)
        {
        }
        
    }
}
