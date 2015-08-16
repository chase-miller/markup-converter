using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Ninject.Activation;
using Ninject.Extensions.Conventions;
using Ninject.Extensions.Conventions.BindingGenerators;
using Ninject.Modules;
using MarkupConverterServiceApi;

namespace FrontlineMarkupLanguagePlugin
{
    public class FrontlinePluginNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IMarkupLanguage>().To<FrontlineMarkupLanguage>();
        }
    }
}
