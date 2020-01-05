using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OOP_LIB
{
    public class CountryCollection
    {
        Thread t2;
        public List<Weather> cityList;
        private string path;
        private CountryCollection()
        {
            cityList = new List<Weather>();
        }
        public CountryCollection(string path) : this()
        {
            this.path = path;
        }
        public void read()
        {
            try
            {
                 t2 = new Thread(() =>
                {
                    var jsonText = File.ReadAllText(path);
                    cityList = JsonConvert.DeserializeObject<List<Weather>>(jsonText);
                    Console.WriteLine(cityList.Count);
                });
                t2.Start();
            }
            catch(IOException ex)
            {
                throw new IOException("CountryCollection: что то не так с путем");
            }
            
            catch (Exception ex) { 
                throw new Exception($"CoutnryCollection: {ex.Message}"); }
        }
        public List<Weather> GetList()
        {
            t2.Join();
            return cityList;
        }

    }
    public struct Weather
    {
        public string name;
        private string country;
    }
}
