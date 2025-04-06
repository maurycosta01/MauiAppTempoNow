using MauiAppTempoNow.Models;
using System.Text.Json;
using System;

namespace MauiAppTempoNow
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        const string apikey = "275e791c559a9b524359de49588b906f"; // 🔐 Substitua pela chave da OpenWeather
        const string urlbase = "https://api.openweathermap.org/data/2.5/weather";

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnBuscarClimaClicked(object sender, EventArgs e)
        {

            string cidade = txt_cidade.Text?.Trim();

            if (string.IsNullOrWhiteSpace(cidade))
            {
                await DisplayAlert("Erro", "Digite o nome de uma cidade", "OK");
                return;
            }

            var tempo = await BuscarClimaDaCidadeAsync(cidade);

            if (tempo != null)
            {
                lbl_res.Text = tempo.InterpretarCondicao();
                lbl_res_tempo.Text = tempo.ToString();
            }
        }

        private Tempo ObterClimaParaCidade(string cidade)
        {
            // Aqui você poderia integrar uma API real. Simulando com variações:
            Random rnd = new Random();
            var descricoes = new[] { "Céu limpo", "Parcialmente nublado", "Chuva leve", "Chuva forte", "Neve" };
            string descricao = descricoes[rnd.Next(descricoes.Length)];

            return new Tempo
            {
                Clima = $"Clima em {cidade}",
                Temperatura = 20 + rnd.NextDouble() * 3,
                description = descricao,
                velocidade = rnd.Next(5, 20),
                visibility = rnd.Next(5000, 10000)
            };

        }

        private async Task<Tempo?> BuscarClimaDaCidadeAsync(string cidade)
        {
            try
            {
                using HttpClient client = new HttpClient();
                string url = $"{urlbase}?q={cidade}&appid={apikey}&lang=pt_br&units=metric";

                HttpResponseMessage response = await client.GetAsync(url);

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await DisplayAlert("Cidade não encontrada", $"A cidade \"{cidade}\" não foi localizada.", "OK");
                    return null;
                }

                response.EnsureSuccessStatusCode(); // lança exceção para 4xx/5xx (exceto 404 que já tratamos)

                string json = await response.Content.ReadAsStringAsync();

                // Desserializar JSON em objeto Tempo
                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;
                var sys = root.GetProperty("sys");

                return new Tempo
                {
                    Clima = $"Clima em {cidade}",
                    description = root.GetProperty("weather")[0].GetProperty("description").GetString(),
                    Temperatura = root.GetProperty("main").GetProperty("temp").GetDouble(),
                    temp_min = root.GetProperty("main").GetProperty("temp_min").GetDouble(),
                    temp_max = root.GetProperty("main").GetProperty("temp_max").GetDouble(),
                    velocidade = root.GetProperty("wind").GetProperty("speed").GetDouble(),
                    visibility = root.GetProperty("visibility").GetInt32(),
                    lat = root.GetProperty("coord").GetProperty("lat").GetDouble(),
                    lon = root.GetProperty("coord").GetProperty("lon").GetDouble()
                };
            }
            catch (HttpRequestException)
            {
                await DisplayAlert("Sem conexão", "Verifique sua conexão com a internet.", "OK");
                return null;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"Erro inesperado: {ex.Message}", "OK");
                return null;
            }
        }
    }

    }
