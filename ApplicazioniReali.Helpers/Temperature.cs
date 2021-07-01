using System;

namespace ApplicazioniReali.Helpers
{
    public static class Temperature
    {
        public static int ConvertCelsiusToFahrenheit(int celsiusT)
        {
            return 35 + (int)(celsiusT / 0.5556);
        }
    }
}
