
namespace Took1.Web.Cloud
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Ria;
    using System.Web.Ria.Data;
    using System.Web.DomainServices;


    // TODO: Create methods containing your application logic.
    [EnableClientAccess()]
    public class HelloWorldDomainService : DomainService
    {
        [ServiceOperation]


        public string HelloWorld(string name)
        {

            return string.Format("Hello {0}, from RIA Services running in the Cloud.", name);

        }
    }
}


