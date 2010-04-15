using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Took1.Silverlight.LifeManager.Data.Model;
using Took1.Silverlight.LifeManager.Data.Source;
using Took1.Silverlight.LifeManager.Data.Collection;

namespace Took1.Silverlight.LifeManager.Data.Controller
{
    public class CheckPointController : BaseController
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

        public CheckPointController(DataSource dataSource)
        {
            base.dataSource = dataSource;
        }

        public CheckPoint Add(CheckPoint checkPoint)
        {
            CheckPoint result = (CheckPoint)base.Add(checkPoint, XmlDataSourceNames.CheckPoint);
            return result;
        }
        public CheckPoint Add(string name)
        {
            CheckPoint checkPoint = new CheckPoint()
            {
                Name = name
            };
            return Add(checkPoint);
        }
        public void Delete(CheckPoint checkPoint)
        {
            if (checkPoint != null)
            {
                if (dataSource.CheckPointEntities.Contains(checkPoint))
                    dataSource.CheckPointEntities.Remove(checkPoint);
            }
        }
        public void Delete(int checkPointID)
        {
            Delete(Get(checkPointID));
        }
        public CheckPointCollection Get()
        {
            return (CheckPointCollection)base.Get(XmlDataSourceNames.CheckPoint);
        }
        public CheckPoint Get(int checkPointID)
        { 
            return (CheckPoint)base.Get(XmlDataSourceNames.CheckPoint,checkPointID);
        }
        public void Update(CheckPoint checkPoint)
        {
        }
        
    }
}
