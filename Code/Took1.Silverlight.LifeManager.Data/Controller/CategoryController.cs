using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Took1.Silverlight.LifeManager.Data.Source;
using Took1.Silverlight.LifeManager.Data.Model;

namespace Took1.Silverlight.LifeManager.Data.Controller
{
    public class CategoryController : BaseController
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

        public CategoryController(DataSource dataSource)
        {
            base.dataSource = dataSource;
        }

        public Category Add(Category transaction)
        {
            Category result = (Category)base.Add(transaction, XmlDataSourceNames.Category);
            return result;
        }
        public Category Add(string name)
        {
            Category category = new Category()
            {
                Name = name
            };
            return Add(category);
        }
        public void Delete(Category category)
        {
            if (category != null)
            {
                if (dataSource.CategoryEntities.Contains(category))
                    dataSource.CategoryEntities.Remove(category);
            }
        }
        public void Delete(int categoryID)
        {
            Delete(Get(categoryID));
        }
        public List<Category> Get()
        {
            return (List<Category>)base.Get(XmlDataSourceNames.Category);
        }
        public Category Get(int categoryID)
        { 
            return (Category)base.Get(XmlDataSourceNames.Category,categoryID);
        }
        public void Update(Category category)
        {
        }

    }
}
