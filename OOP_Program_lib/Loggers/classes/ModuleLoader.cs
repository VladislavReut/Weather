using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;


namespace OOP_LIB
{
    static class ModuleLoader
    {
        public static void Load(IKernel kernel, LoggerType logger)
        {
            if(LoggerType.Console==logger)
            kernel.Bind<ILogger>().To<ConsoleLogger>().InSingletonScope();
            else
            kernel.Bind<ILogger>().To<FileLogger>().InSingletonScope();
            kernel.Bind<IReflector>().To<Reflector>();
            kernel.Bind<IThreadManager>().To<ThreadManager>().InSingletonScope();
        }
    }
}
