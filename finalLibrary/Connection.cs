using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace OOP_LIB
{
    public class Connection
    {
        public Forecast GetWeatherForecast(string city)
        {
            using (WebClient client = new WebClient())
            {
                string uri =$"http://api.openweathermap.org/data/2.5/weather?q={city}&APPID=891e6e3996e327dca3a2f54e56e51381";
                try
                {

                string xmlContent =  client.DownloadString(uri);
                Dictionary<string, string> keyValues = ParseWeather(xmlContent);
                Forecast forecast = new Forecast();
                keyValues.TryGetValue("temperature", out forecast.temperature);
                keyValues.TryGetValue("country", out forecast.country);
                keyValues.TryGetValue("humidity", out forecast.humidity);
                keyValues.TryGetValue("description", out forecast.description);
                keyValues.TryGetValue("name", out forecast.name);
                forecast.dateTime = DateTime.Now;

                return forecast;
                }
                catch(Exception ex)
                {
                    throw new FileLogger($"Connection:{ex.Message}");
                }
            }
        }
        private Dictionary<string, string> ParseWeather(string jsonText)
        {
            JObject jObject = JObject.Parse(jsonText);
                float temp = jObject["main"]["temp"].ToObject<float>();
                temp = temp - 280;
                string temperature = temp.ToString();
                string humidity = jObject["main"]["humidity"].ToString();
                string description = jObject["weather"][0]["description"].ToString();
                string country = jObject["sys"]["country"].ToString();
                string city = jObject["name"].ToString();

                Dictionary<string, string> keyValues = new Dictionary<string, string>();
            try
            {
                keyValues.Add("temperature", temperature);
                keyValues.Add("humidity", humidity);
                keyValues.Add("description", description);
                keyValues.Add("country", country);
                keyValues.Add("name", city);
                return keyValues;
            }
            catch (ArgumentException ex)
            {
                throw new FileLogger($"Connection:{ex.Message}");
            }
        }
    }
    public struct Forecast
    {
        public string country;
        public string temperature;
        public string description;
        public string humidity;
        public string name;
        public DateTime dateTime;
    }
}

