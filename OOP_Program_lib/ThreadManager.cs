using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace OOP_LIB
{
   public class ThreadManager : IThreadManager
    {
        public void StartInNewThread(ThreadStart action)
        {
            new Thread(action).Start();

           
        }

        public void StartInNewThread<T>(Action <T>action, T item)
        {
            object[] args = { item };
         
            //new Thread(action.DynamicInvoke(args)).Start(item);
            new Thread(() =>
            {
                action.DynamicInvoke(item);
            }).Start();


            new Thread(() =>
            {
                action.DynamicInvoke(args);
            }).Start();
            

        }
    }
}
