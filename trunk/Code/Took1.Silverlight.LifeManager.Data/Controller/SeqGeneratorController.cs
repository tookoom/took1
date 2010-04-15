using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Took1.Silverlight.LifeManager.Data.Model;
using Took1.Silverlight.LifeManager.Data.Source;
using Took1.Silverlight.LifeManager.Data.Collection;

namespace Took1.Silverlight.LifeManager.Data.Controller
{
    public class SeqGeneratorController : BaseController
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

        public SeqGeneratorController(DataSource dataSource)
        {
            base.dataSource = dataSource;
        }

        public int GetNextValue(string name)
        {
            int result = 0;
            SeqGenerator seqGenerator = (from el in dataSource.SeqGeneratorEntities
                                         where el.Name == name
                                         select el).FirstOrDefault();
            if (seqGenerator != null)
            {
                seqGenerator.Value++;
                result = seqGenerator.Value;
            }
            return result;
        }

        public SeqGenerator Add(SeqGenerator seqGenerator)
        {
            SeqGenerator result = (SeqGenerator)base.Add(seqGenerator, XmlDataSourceNames.SeqGenerator);
            return result;
        }
        public SeqGenerator Add(string name, int value)
        {
            SeqGenerator seqGenerator = new SeqGenerator()
            {
                Name = name,
                Value = value
            };
            return Add(seqGenerator);
        }
        public void Delete(SeqGenerator seqGenerator)
        {
            if (seqGenerator != null)
            {
                if (dataSource.SeqGeneratorEntities.Contains(seqGenerator))
                    dataSource.SeqGeneratorEntities.Remove(seqGenerator);
            }
        }
        public void Delete(int seqGeneratorID)
        {
            Delete(Get(seqGeneratorID));
        }
        public SeqGeneratorCollection Get()
        {
            return (SeqGeneratorCollection)base.Get(XmlDataSourceNames.SeqGenerator);
        }
        public SeqGenerator Get(int seqGeneratorID)
        {
            return (SeqGenerator)base.Get(XmlDataSourceNames.SeqGenerator, seqGeneratorID);
        }
        public void Update(Account account)
        {
        }
    }
}
