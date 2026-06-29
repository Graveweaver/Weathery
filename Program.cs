using System.CommandLine;
using System.Net.Http.Json;
using Weathery.Models;

namespace Weathery;

class Program
{
    public static readonly HttpClient Http = new() { Timeout = TimeSpan.FromSeconds(10) };

    static void Print(WeatherResponse wr)
    {
        Console.WriteLine($"Temperature: {wr.current.temperature_2m} {wr.current_units.temperature_2m}");
        Console.WriteLine($"Relative humidity: {wr.current.relative_humidity_2m} {wr.current_units.relative_humidity_2m}");
        Console.WriteLine($"Wind speed: {wr.current.wind_speed_10m} {wr.current_units.wind_speed_10m}");
        Console.WriteLine($"Wind direction: {wr.current.wind_direction_10m} {wr.current_units.wind_direction_10m}");
        if (wr.current.precipitation > 0)
        {
            Console.WriteLine($"Precipitation: {wr.current.precipitation} {wr.current_units.precipitation}");
            Console.WriteLine($"Rain: {wr.current.rain} {wr.current_units.rain}");
        }
        
    }
    static void Main(string[] args)
    {
        string getcityinfourl = string.Empty;
        string getweatherurl = string.Empty;
        Option<string> cityOption = new("-city")
        {
            Description = "City name to get the weather for"
        };

        RootCommand rootCommand = new("weathery");
        rootCommand.Add(cityOption);
        
        rootCommand.SetAction(parseResult =>
        {
           getcityinfourl = "https://geocoding-api.open-meteo.com/v1/search?name={name}&language=en&format=json".Replace("{name}", parseResult.GetValue(cityOption));
        });
        ParseResult parseResult = rootCommand.Parse(args);
        parseResult.Invoke();
        
        Geocoding? geocodingResponse = Http.GetFromJsonAsync<Geocoding>(getcityinfourl).Result;
        Location? location = geocodingResponse?.results
            .Select(x => new Location(x.name, x.latitude, x.longitude))
            .FirstOrDefault(x => x.Name.ToLower() == parseResult.GetValue(cityOption)?.ToLower());
        
        if(location == null)
        {
            Console.WriteLine("City not found");
            return;
        }

        getweatherurl =
            "https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}&current=precipitation,temperature_2m,relative_humidity_2m,weather_code,wind_speed_10m,wind_direction_10m,rain"
            .Replace("{lat}", location.Latitude.ToString())
            .Replace("{lon}", location.Longitude.ToString());
        WeatherResponse wr = Http.GetFromJsonAsync<WeatherResponse>(getweatherurl).Result;
        Print(wr);
    }
}
