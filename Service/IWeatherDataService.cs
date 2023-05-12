using MP3Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp3.Service
{
    public interface IWeatherDataService
    {
        public Task<List<WeatherForecast>> GetWeatherDataService();
    }
}
