using FrontlineMarkupLanguagePlugin;
using MarkupConverterServiceApi;
using MarkupConverterServiceLocal;
using Ninject;
using Ninject.Activation;
using Ninject.Extensions.Conventions;
using Ninject.Extensions.Conventions.BindingGenerators;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MarkupConverter.ModulesNinjectModules
{
    public class NinjectModules : NinjectModule
    {
        public override void Load()
        {
            // This can easily be replaced with, say, a proxy to a microservice.  
            Bind<IMarkupConverterService>().To<MarkupConverterServiceLocal.MarkupConverterServiceLocal>();

            // For ease of building, we'll add this plugin as a reference. 
            Bind<IMarkupLanguage>().To<FrontlineMarkupLanguage>();

            string path = Path.GetDirectoryName(Assembly.GetAssembly(typeof(MarkupConverterServiceApi.IMarkupConverterService)).Location);

            // Find any plugins that 3rd parties may have added.
            this.Kernel.Bind(scanner => scanner.FromAssembliesInPath(path)
                                   .SelectAllClasses()
                                   .InheritedFrom<IMarkupLanguage>()
                                   .BindAllInterfaces());
        }
    }
}
