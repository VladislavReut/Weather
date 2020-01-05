using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LIB
{
    public enum LoggerType
    {
        File, Console
    }
    static class LoggerFactory
    {
        public static LoggerType loggerType = LoggerType.Console;
        public static ILogger GetLogger()
        {
            switch (loggerType)
            {
                case LoggerType.File:
                    {
                        return new FileLogger();
                    }
                case LoggerType.Console: { return new ConsoleLogger(); }
                default: { return new  FileLogger(); }
            }

        }

    }
}
