using Weathery.Models;

namespace Weathery;

public enum Condition
{
    Unknown,
    Sunny,
    PartlyCloudy,
    Cloudy,
    VeryCloudy,
    LightShowers,
    HeavyShowers,
    LightSnow,
    HeavySnow,
    Thunderstorm,
    Fog
}

public class AsciiIcons
{
    public static string[] GetIcon(int weathercode)
    {
        if (Icons.TryGetValue(GetWeatherCondition(weathercode), out var art))
            return art;
        return Icons[Condition.Unknown];
    }

    private static Condition GetWeatherCondition(int weathercode)
    {
        return weathercode switch
        {
            0 => Condition.Sunny,
            1 or 2 => Condition.PartlyCloudy,
            3 => Condition.Cloudy,
            45 or 48 => Condition.Fog,
            51 or 53 or 55 or 61 or 80 => Condition.LightShowers,
            56 or 57 or 66 or 71 or 85 => Condition.LightSnow,
            63 or 65 or 81 or 82 => Condition.HeavyShowers,
            67 or 73 or 75 or 77 or 86 => Condition.HeavySnow,
            95 => Condition.Thunderstorm,
            _ => Condition.Unknown
        };
    }

    private static readonly Dictionary<Condition, string[]> Icons = new()
    {
        [Condition.Unknown] = new[]
        {
            "             ",
            "    .-.      ",
            "     __)     ",
            "    (        ",
            "     `-’     ",
            "      •      ",
            "             ",
        },

        [Condition.Sunny] = new[]
        {
            "             ",
            "\u001b[38;5;226m    \\   /    \u001b[0m",
            "\u001b[38;5;226m     .-.     \u001b[0m",
            "\u001b[38;5;226m  ― (   ) ―  \u001b[0m",
            "\u001b[38;5;226m     `-’     \u001b[0m",
            "\u001b[38;5;226m    /   \\    \u001b[0m",
            "             ",
        },

        [Condition.PartlyCloudy] = new[]
        {
            "             ",
            "\u001b[38;5;226m   \\  /\u001b[0m      ",
            "\u001b[38;5;226m _ /\"\"\u001b[38;5;250m.-.    \u001b[0m",
            "\u001b[38;5;226m   \\_\u001b[38;5;250m(   ).  \u001b[0m",
            "\u001b[38;5;226m   /\u001b[38;5;250m(___(__) \u001b[0m",
            "             ",
            "             ",
        },

        [Condition.Cloudy] = new[]
        {
            "             ",
            "             ",
            "\u001b[38;5;250m     .--.    \u001b[0m",
            "\u001b[38;5;250m  .-(    ).  \u001b[0m",
            "\u001b[38;5;250m (___.__)__) \u001b[0m",
            "             ",
            "             ",
        },

        [Condition.VeryCloudy] = new[]
        {
            "             ",
            "             ",
            "\u001b[38;5;240;1m     .--.    \u001b[0m",
            "\u001b[38;5;240;1m  .-(    ).  \u001b[0m",
            "\u001b[38;5;240;1m (___.__)__) \u001b[0m",
            "             ",
            "             ",
        },

        [Condition.LightShowers] = new[]
        {
            "             ",
            "\u001b[38;5;226m _`/\"\"\u001b[38;5;250m.-.    \u001b[0m",
            "\u001b[38;5;226m  ,\\_\u001b[38;5;250m(   ).  \u001b[0m",
            "\u001b[38;5;226m   /\u001b[38;5;250m(___(__) \u001b[0m",
            "\u001b[38;5;111m     ' ' ' ' \u001b[0m",
            "\u001b[38;5;111m    ' ' ' '  \u001b[0m",
            "             ",
        },

        [Condition.HeavyShowers] = new[]
        {
            "             ",
            "\u001b[38;5;226m _`/\"\"\u001b[38;5;240;1m.-.    \u001b[0m",
            "\u001b[38;5;226m  ,\\_\u001b[38;5;240;1m(   ).  \u001b[0m",
            "\u001b[38;5;226m   /\u001b[38;5;240;1m(___(__) \u001b[0m",
            "\u001b[38;5;21;1m   ‚'‚'‚'‚'  \u001b[0m",
            "\u001b[38;5;21;1m   ‚'‚'‚'‚'  \u001b[0m",
            "             ",
        },

        [Condition.LightSnow] = new[]
        {
            "             ",
            "\u001b[38;5;250m     .-.     \u001b[0m",
            "\u001b[38;5;250m    (   ).   \u001b[0m",
            "\u001b[38;5;250m   (___(__)  \u001b[0m",
            "\u001b[38;5;255m    *  *  *  \u001b[0m",
            "\u001b[38;5;255m   *  *  *   \u001b[0m",
            "             ",
        },

        [Condition.HeavySnow] = new[]
        {
            "             ",
            "\u001b[38;5;240;1m     .-.     \u001b[0m",
            "\u001b[38;5;240;1m    (   ).   \u001b[0m",
            "\u001b[38;5;240;1m   (___(__)  \u001b[0m",
            "\u001b[38;5;255;1m   * * * *   \u001b[0m",
            "\u001b[38;5;255;1m  * * * *    \u001b[0m",
            "             ",
        },

        [Condition.Thunderstorm] = new[]
        {
            "             ",
            "\u001b[38;5;240;1m     .-.     \u001b[0m",
            "\u001b[38;5;240;1m    (   ).   \u001b[0m",
            "\u001b[38;5;240;1m   (___(__)  \u001b[0m",
            "\u001b[38;5;228;5m    ⚡\u001b[38;5;111;25m\"\"\u001b[38;5;228;5m⚡\u001b[38;5;111;25m\"\" \u001b[0m",
            "\u001b[38;5;21;1m  ‚'‚'‚'‚'   \u001b[0m",
            "             ",
        },

        [Condition.Fog] = new[]
        {
            "             ",
            "             ",
            "\u001b[38;5;251m _ - _ - _ - \u001b[0m",
            "\u001b[38;5;251m  _ - _ - _  \u001b[0m",
            "\u001b[38;5;251m _ - _ - _ - \u001b[0m",
            "             ",
            "             ",
        },
    };
}