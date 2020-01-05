
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LIB
{
    class ConsoleLogger:ILogger
    {
      
      public ConsoleLogger() { }
      
        public void Log(Exception ex)
        {
            Console.WriteLine($"{DateTime.Now}, Error, {ex.Message}");
        }

        public void Log(TypeError typeError, string message)
        {
            Console.WriteLine($"{DateTime.Now}, {typeError}, {message}");
        }

        public void Log(TypeError typeError, string message, ObjInfo objInfo)
        {
            Log(typeError, message);
            Console.WriteLine(objInfo);
        }
    }
}
