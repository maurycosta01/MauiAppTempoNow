using MauiAppTempoNow.Models;
using Newtonsoft.Json.Linq;


namespace MauiAppTempoNow.Services;

public class WeatherService
{
    public static async Task<Tempo?> GetPrevisao (string cidade)
    {
        Tempo? t = null;

        string chave = "275e791c559a9b524359de49588b906f";//imprementar a chave api 

        string url = $"http://api.openweathermap.org/data/2.5/weather?" + $"q={cidade}&appid={chave}&units=metric";

        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage resp = await client.GetAsync(url);

            if (resp.IsSuccessStatusCode)
            {
                string json = await resp.Content.ReadAsStringAsync();
                var racunho = JObject.Parse(json);

                DateTime time = new();
                DateTime sunrise = time.AddSeconds((double)racunho["sys"]["sunrise"]).ToLocalTime();
                DateTime sunset = time.AddSeconds((double)racunho["sys"]["sunset"]).ToLocalTime();

                t = new()
                {
                    lat = (double)racunho["coord"]["lat"],
                    lon = (double)racunho["coord"]["lon"],
                    description = (string)racunho["weather"][0]["description"],
                    main = (string)racunho["weather"][0]["main"],
                    temp_min = (double)racunho["main"]["temp_min"],
                    temp_max = (double)racunho["main"]["temp_max"],
                    visibility = (int)racunho["visibility"],
                    sunrise = sunrise.ToString(""),
                    sunset = sunset.ToString(""),
                }; // fecha o objeto Tempo
            }// fecha o if se for sucesso
        }// fecha laço using
        return t;
    }
}
