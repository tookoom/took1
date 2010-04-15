using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Took1.Silverlight.LifeManager.Data.Model;
using Took1.Silverlight.LifeManager.Data.Source;

namespace Took1.Silverlight.LifeManager.Data.Controller
{
    public class EventController : BaseController
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

        public EventController(DataSource dataSource)
        {
            base.dataSource = dataSource;
        }

        public Event Add(Event transaction)
        {
            Event result = (Event)base.Add(transaction, XmlDataSourceNames.Event);
            return result;
        }
        public Event Add(string name, DateTime eventTimeStamp, DateTime registryTimeStamp)
        {
            Event ev = new Event()
            {
                Name = name,
                EventTimeStamp = eventTimeStamp,
                RegistryTimeStamp = registryTimeStamp
            };
            return Add(ev);
        }
        public void Delete(Event ev)
        {
            if (ev != null)
            {
                if (dataSource.EventEntities.Contains(ev))
                    dataSource.EventEntities.Remove(ev);
            }
        }
        public void Delete(int evID)
        {
            Delete(Get(evID));
        }
        public List<Event> Get()
        {
            return (List<Event>)base.Get(XmlDataSourceNames.Event);
        }
        public Event Get(int evID)
        { 
            return (Event)base.Get(XmlDataSourceNames.Event,evID);
        }
        public void Update(Event ev)
        {
        }
    }
}
