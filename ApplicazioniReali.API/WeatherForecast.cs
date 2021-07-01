using System;
using ApplicazioniReali.Helpers;

namespace ApplicazioniReali.API
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => Temperature.ConvertCelsiusToFahrenheit(TemperatureC);

        public string Summary { get; set; }
    }
}
