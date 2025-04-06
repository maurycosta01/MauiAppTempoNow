namespace MauiAppTempoNow.Models;

    public class Tempo : ContentPage
    {
        public class TempoDetails
        {
            public MainInfo Main { get; set; }
            public List<WeatherInfo> Weather { get; set; }

            public class MainInfo
            {
                public double Temp { get; set; }
                public double Humidity { get; set; }
            }

            public class WeatherInfo
            {
                public string Description { get; set; }
            }
        }
    }

