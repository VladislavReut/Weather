using System;
using System.IO;
using System.Threading;
using Newtonsoft.Json;
namespace OOP_LIB
{
    public class Cache
    {
        string directory = @"C:\Users\Public\Documents\cache";
        public void WriteToCache(Forecast forecast)
        {
            try
            {

                using (StreamWriter sw = File.CreateText($"{directory}\\{forecast.name}.json"))
                {
                    string wether = JsonConvert.SerializeObject(forecast, Formatting.Indented);
                    sw.Write(wether);
                }

            }
            catch (IOException ex)
            {
                throw new FileLogger($"Cache:{ex.Message}");
            }
        }
        public void DeleteFromCache()
        {
            int i = 0;
            TimerCallback tm = new TimerCallback((item) =>
            {
            DirectoryInfo directoryInfo = new DirectoryInfo(directory);
            var files = directoryInfo.GetFiles();
            foreach (var file in files)
            {
                DateTime date = file.CreationTime.AddHours(2.0);
                if (date < DateTime.Now)
                    file.Delete();
            }
            }
            );
             
              Timer timer = new Timer(tm, null, 0, 7200000);//(720 000) = 2 часа
        }
        public bool GetFromCache(string city, ref Forecast forecast)
        {
            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(directory);
                var files = directoryInfo.GetFiles();
                foreach (var file in files)
                {
                    if (file.Name.Contains(city))
                    {
                        using (StreamReader reader = File.OpenText(file.FullName))
                        {
                            string JSonText = reader.ReadToEnd();
                            forecast = JsonConvert.DeserializeObject<Forecast>(JSonText);
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (IOException ex)
            {
                throw new FileLogger($"Cache:{ex.Message}");
            }
            catch (Exception ex)
            {
                throw new FileLogger($"Cache:{ex.Message}");
            }
        }
    }
}
