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
using Took1.Silverlight.LifeManager.Data.Controller;

namespace Took1.Silverlight.LifeManager.Data.Model
{
    public class CheckPointAccount : BaseModel
    {
        #region PRIVATE MEMBERS
        private CheckPoint checkPoint = null;
        private Account account = null;

        #endregion
        #region PUBLIC PROPERTIES
        public int CheckPointAccountID { get; set; }
        public int CheckPointID { get; set; }
        public int AccountID { get; set; }
        public float Value { get; set; }

        public CheckPoint CheckPoint
        {
            get
            {
                loadCheckPointReference();
                return checkPoint;
            }
            set
            {
                checkPoint = value;
                if (checkPoint != null)
                {
                    if (!DataSource.CheckPointEntities.Contains(checkPoint))
                        DataSource.CheckPointEntities.Add(checkPoint);
                    CheckPointID = checkPoint.CheckPointID;
                }
            }
        }
        public Account Account
        {
            get
            {
                loadAccountReference();
                return account;
            }
            set
            {
                account = value;
                if (account != null)
                {
                    if (!DataSource.AccountEntities.Contains(account))
                        DataSource.AccountEntities.Add(account);
                    AccountID = account.AccountID;
                }
            }
        }

        #endregion

        private void loadCheckPointReference()
        {
            if (DataSource != null)
            {
                CheckPointController transactionController = new CheckPointController(DataSource);
                checkPoint = transactionController.Get(CheckPointID);
            }
        }
        private void loadAccountReference()
        {
            if (DataSource != null)
            {
                AccountController accountController = new AccountController(DataSource);
                account = accountController.Get(AccountID);
            }
        }
    }
}
