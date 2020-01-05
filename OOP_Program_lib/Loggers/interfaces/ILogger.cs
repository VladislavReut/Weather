using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LIB
{
   public enum TypeError {
    Error, Warning, information}
    interface ILogger
    {
         void Log(Exception ex);
         void Log(TypeError typeError, string message);
        void Log(TypeError typeError, string message, ObjInfo objInfo);
    }
}
