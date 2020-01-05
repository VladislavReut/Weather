using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ninject;
using Ninject.Modules;

namespace OOP_LIB
{
    public class WeatherStatus
    {
        IKernel kernel;
        Forecast forecast;
        public WeatherStatus()
        {
               kernel = new StandardKernel();
               Load(kernel);
               kernel.Get<Cache>().DeleteFromCache();
        }

        public Forecast GetForecast(string city)
        {
            if (kernel.Get<Cache>().GetFromCache(city, ref forecast))
            {
                return forecast;
            }
            else
            {
                forecast = kernel.Get<Connection>().GetWeatherForecast(city);
                Thread t2 = new Thread(() =>
                kernel.Get<Cache>().WriteToCache(forecast));
                t2.Start();
                return forecast;
            }
        }
        public List<Weather> GetWeatherList()
        {
            kernel.Get<CountryCollection>().read();

            return kernel.Get<CountryCollection>().GetList();

        }

        void Load(IKernel kernel)
        {
            kernel.Bind<Connection>().To<Connection>().InSingletonScope();
            kernel.Bind<CountryCollection>().To<CountryCollection>().InSingletonScope().WithConstructorArgument("path", @"C:\Users\Public\Documents\city.json"); ;
            kernel.Bind<Cache>().To<Cache>().InSingletonScope();
        }
    }
}
