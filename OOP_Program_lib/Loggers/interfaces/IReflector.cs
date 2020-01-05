using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LIB
{
    interface IReflector
    {
        ObjInfo Analyze(Object obj);
        void Save(ObjInfo info);
    }
}
