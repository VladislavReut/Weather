using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LIB
{
    public class FileLogger:Exception, ILogger
    {
        
        public FileLogger() { }
        public FileLogger(string message) {
            Log(TypeError.Error, message);
        }

        string path;
        public void Log(Exception ex)
        {
            path = "C:\\Users\\Public\\Documents\\" + ex.Source;
            File.AppendAllText(path, $"{DateTime.Now}, Error, {ex.Message}");
        }

        public void Log(TypeError typeError, string message)
        {
            path = $"C:\\Users\\Public\\Documents\\{this.GetType()}.json";
            File.AppendAllText(path, $"{DateTime.Now}, {typeError}, {message}");
        }

        public void Log(TypeError typeError, string message, ObjInfo objInfo)
        {
            Log(typeError, message);
            path = $"C:\\Users\\Public\\Documents\\{objInfo.typeName}.json";

            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.Write(JsonConvert.SerializeObject(objInfo));
            }
        }
        public static void ToFile(string text) {
            using (StreamWriter sw = File.CreateText("C:\\Users\\Public\\Documents\\text.txt")) 
            {
                sw.Write(text);
            }
        }
    }
}

