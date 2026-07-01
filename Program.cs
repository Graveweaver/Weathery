using System.CommandLine;
using System.Net.Http.Json;
using Weathery.Models;

namespace Weathery;

class Program
{
    public static readonly HttpClient Http = new() { Timeout = TimeSpan.FromSeconds(10) };

    static void Print(WeatherResponse wr)
    {
        var lines = AsciiIcons.GetIcon(wr.current.weather_code);
        var stats = new List<string>
        {
            $"Temperature: {ColorizeTemperature(wr.current.temperature_2m)} {wr.current_units.temperature_2m} (feels like {ColorizeTemperature(wr.current.apparent_temperature)} {wr.current_units.apparent_temperature})",
            $"Humidity: \u001b[0;96m{wr.current.relative_humidity_2m} {wr.current_units.relative_humidity_2m}\u001b[0m",
            $"Wind: {ColorizeWindSpeed(wr.current.wind_speed_10m)} {wr.current_units.wind_speed_10m} {GetWindArrow(wr.current.wind_direction_10m)}"
        };
        if (wr.current.precipitation > 0)
        {
            stats.Add($"Precipitation: {wr.current.precipitation} {wr.current_units.precipitation}");
            stats.Add($"Rain: {wr.current.rain} {wr.current_units.rain}");
        }
        
        // just eyeballing what looks good
        int leftWidth = 20;
        int rowCount = Math.Max(lines.Length, stats.Count);
        int currentstatsrow = 0;
        
        for (int i = 0; i < rowCount; i++)
        {
            //if the line is out of bounds, use "nothing"
            string left = i < lines.Length ? lines[i] : "";
            string right = "";
            if (i > 0 && i < stats.Count + 1)
            {
                right = stats[currentstatsrow++];
            }
            Console.WriteLine(left.PadRight(leftWidth) + right);
        }
    }
    
    static string GetWindArrow(int windDirectionDegrees)
    {
        string[] directions = new []{
            "↑ (N)",
            "↗ (NE)",
            "→ (E)",
            "↘ (SE)",
            "↓ (S)",
            "↙ (SW)",
            "← (W)",
            "↖ (NW)"
        };
        int index = Convert.ToInt32(((float)windDirectionDegrees + 22.5) / 45) % 8;
        return directions[index];
    }
    
    //Colorize stats based on "severity"
    static string ColorizeTemperature(double temp)
    {
        string color = temp switch
        {
            < 0 => "\u001b[0;34m",      // Blue
            < 18 => "\u001b[0;36m",     // Cyan
            < 29 => "\u001b[0;32m",     // Green
            < 35 => "\u001b[0;33m",     // Yellow
            _ => "\u001b[0;31m"         // Red
        };
        return $"{color}{temp}\u001b[0m";
    }
    static string ColorizeWindSpeed(double windSpeed)
    {
        string color = windSpeed switch
        {
            < 10 => "\u001b[0;32m",     // Green
            < 20 => "\u001b[0;36m",     // Cyan
            < 45 => "\u001b[0;33m",     // Yellow
            _ => "\u001b[0;31m"         // Red
        };
        return $"{color}{windSpeed}\u001b[0m";
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
            "https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}&hourly=temperature_2m&current=weather_code,apparent_temperature,rain,precipitation,temperature_2m,relative_humidity_2m,wind_speed_10m,wind_direction_10m,is_day,showers,snowfall,cloud_cover,pressure_msl,surface_pressure,wind_gusts_10m"
            .Replace("{lat}", location.Latitude.ToString())
            .Replace("{lon}", location.Longitude.ToString());
        
        WeatherResponse? wr = Http.GetFromJsonAsync<WeatherResponse>(getweatherurl).Result;
        if(wr is null)
        {
            Console.WriteLine("Error getting weather data");
            return;
        }
        Print(wr);
    }
}
