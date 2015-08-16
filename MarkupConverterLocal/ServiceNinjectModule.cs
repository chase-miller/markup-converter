using FrontlineMarkupLanguagePlugin;
using MarkupConverterServiceApi;
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

namespace MarkupConverterServiceLocal
{
    public class ServiceNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IMarkupConverterService>().To<MarkupConverterServiceLocal>();

            this.Kernel.Load(Assembly.GetAssembly(typeof(FrontlineMarkupLanguage)));

            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            // Find any plugins that 3rd parties may have added.
            this.Kernel.Bind(scanner => scanner.FromAssembliesInPath(path)
                                   .SelectAllClasses()
                                   .InheritedFrom<IMarkupLanguage>()
                                   .BindDefaultInterface());
        }
    }
}
