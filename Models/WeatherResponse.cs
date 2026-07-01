namespace Weathery.Models;

public class WeatherResponse
{
    public double? latitude { get; set; }
    public double? longitude { get; set; }
    public double? generationtime_ms { get; set; }
    public int? utc_offset_seconds { get; set; }
    public string? timezone { get; set; }
    public string? timezone_abbreviation { get; set; }
    public double? elevation { get; set; }
    public Current_units? current_units { get; set; }
    public Current? current { get; set; }
    public Hourly_units? hourly_units { get; set; }
    public Hourly? hourly { get; set; }

    public class Current_units
    {
        public string? time { get; set; }
        public string? interval { get; set; }
        public string? weather_code { get; set; }
        public string? apparent_temperature { get; set; }
        public string? rain { get; set; }
        public string? precipitation { get; set; }
        public string? temperature_2m { get; set; }
        public string? relative_humidity_2m { get; set; }
        public string? wind_speed_10m { get; set; }
        public string? wind_direction_10m { get; set; }
        public string? is_day { get; set; }
        public string? showers { get; set; }
        public string? snowfall { get; set; }
        public string? cloud_cover { get; set; }
        public string? pressure_msl { get; set; }
        public string? surface_pressure { get; set; }
        public string? wind_gusts_10m { get; set; }
    }

    public class Current
    {
        public string? time { get; set; }
        public int? interval { get; set; }
        public int? weather_code { get; set; }
        public double? apparent_temperature { get; set; }
        public double? rain { get; set; }
        public double? precipitation { get; set; }
        public double? temperature_2m { get; set; }
        public int? relative_humidity_2m { get; set; }
        public double? wind_speed_10m { get; set; }
        public int? wind_direction_10m { get; set; }
        public int? is_day { get; set; }
        public double? showers { get; set; }
        public double? snowfall { get; set; }
        public int? cloud_cover { get; set; }
        public double? pressure_msl { get; set; }
        public double? surface_pressure { get; set; }
        public double? wind_gusts_10m { get; set; }
    }

    public class Hourly_units
    {
        public string? time { get; set; }
        public string? temperature_2m { get; set; }
    }

    public class Hourly
    {
        public string[]? time { get; set; }
        public double[]? temperature_2m { get; set; }
    }
}