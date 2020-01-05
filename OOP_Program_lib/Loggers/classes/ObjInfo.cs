
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LIB
{
  public  struct ObjInfo
    {
        public IEnumerable<ConstructorInfo> cons;
        public string typeName;
        public string sborka;
        public Type parent;
        public IEnumerable<string> namePublicFunc;
        public IEnumerable<string> namePublicFields;
        public IEnumerable<string> namePublicProperties;
        public IEnumerable<string> namePrivateProperties;
        public IEnumerable<string> namePrivateFields;
        public IEnumerable<string> namePrivateFunc;
    }
   public class Reflector : IReflector
    {
        public ObjInfo Analyze( object inf)
        {

            ObjInfo info = new ObjInfo();
            Type type = inf.GetType();
            info.typeName = type.FullName;
            info.cons = type.GetConstructors();
            info.parent = type.BaseType;
            info.sborka = type.AssemblyQualifiedName;
            info.namePublicFunc = type.GetMethods().AsEnumerable().Select(n=>n.Name);
            info.namePublicFields = type.GetFields().AsEnumerable().Select(n => n.Name);
            info.namePublicProperties = type.GetProperties().AsEnumerable().Select(n => n.Name);
            info.namePrivateFunc = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).AsEnumerable().Select(n => n.Name);
            info.namePrivateFields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance).AsEnumerable().Select(n => n.Name);
            info.namePrivateProperties = type.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance).AsEnumerable().Select(n => n.Name);
            return info;
        }

        public static object Create<T>(T cl, object[] param) where T : class
        {
            Type type = cl.GetType();
            //return Activator.CreateInstance(type);
            var cons = type.GetTypeInfo().GetConstructors().AsEnumerable();
            foreach (ConstructorInfo ke in cons)
            {
                object ret = ke.Invoke(BindingFlags.CreateInstance, null, param, null);

                if (ret != null)
                    return (T)ret;
            }
            return -1;
        }

        public static void Invoke<T>(T obj, string method, object[] param) where T : class
        {
            Type type = obj.GetType();
            var met = type.GetMethod(method);
            if (met == null)
                throw new NullReferenceException("Where is method?");
            type.InvokeMember(method, BindingFlags.InvokeMethod, Type.DefaultBinder, obj, param);

        }
        public static void Invoke<T>(T obj, string method, object[] param, BindingFlags flags) where T : class
        {
            Type type = obj.GetType();
            var met = type.GetMethod(method, flags);
            if (met == null)
                throw new NullReferenceException("Where is method?");
            type.InvokeMember(method, BindingFlags.InvokeMethod, Type.DefaultBinder, obj, param);

        }
        public static void Invoke<T>(T obj, string method, object[] param, object[] paramConst) where T : class
        {
            T perem = (T)Create<T>(obj, paramConst);
            Reflector.Invoke(perem, method, param);
        }
        public static void Invoke<T>(T obj, string method, object[] param, object[] paramConst, BindingFlags flags) where T : class
        {
            T perem = (T)Create<T>(obj, paramConst);
            Reflector.Invoke(perem, method, param, flags);
        }

        public void Save(ObjInfo info)
        {
            string path = $"C:\\Users\\Public\\Documents\\{info.typeName}.json";

            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.Write(JsonConvert.SerializeObject(info));
            }


        }

    }
}
