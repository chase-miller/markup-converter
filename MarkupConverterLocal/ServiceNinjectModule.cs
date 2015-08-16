using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using MarkupConverterServiceApi;

namespace MarkupConverterServiceLocal
{
    public class ServiceNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IMarkupConverterService>().To<MarkupConverterServiceLocal>();
        }
    }
}
