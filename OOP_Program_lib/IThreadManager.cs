using System;
using System.Threading;

namespace OOP_LIB
{
     interface IThreadManager
    {
        void StartInNewThread(  ThreadStart action);
        void StartInNewThread<T>(Action <T>action,T item);
        
    }
}