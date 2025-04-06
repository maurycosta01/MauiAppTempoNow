namespace MauiAppTempoNow.Models;

public class Tempo
{ 
    public string Clima { get; set; } 

    public double Temperatura { get; set; }

    public double velocidade { get; set; }

    public double? lon { get; set; }
    public double? lat { get; set; }
    public double? temp_min { get; set; }
    public double? temp_max { get; set; }
    public int? visibility { get; set; }
    public double? speed { get; set; }
    public string? main { get; set; }
    public string? description { get; set; }
    public string? sunrise { get; set; }
    public string? sunset { get; set; }

    public override string ToString()
    {
        return $"Clima: {Clima}\n" +
               $"Temperatura: {Temperatura}°C\n" +
               $"Descrição: {description}\n" +
               $"Vento: {velocidade} km/h\n" +
               $"Visibilidade: {visibility / 1000.0} km\n" +
               $"Latitude: {lat} \n" +
               $"Longitude: {lon} \n" +
               $"Temp Máx: {temp_max} \n" +
               $"Temp Min: {temp_min} \n";

        ;
    }
    public string InterpretarCondicao()
    {
        if (description.ToLower().Contains("sol"))
            return "Hoje está ensolarado!";
        if (description.ToLower().Contains("nublado"))
            return "Hoje está nublado.";
        if (description.ToLower().Contains("chuva"))
            return "Hoje está chuvoso!";
        if (description.ToLower().Contains("neve"))
            return "Hoje está nevando!";

        return "Condição climática não identificada.";
    }
}

