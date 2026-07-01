# Weathery

Pretty terminal weather, powered by [Open-Meteo](https://open-meteo.com).

```text
$ weathery -city budapest

    \   /     Temperature: 28°C (feels like 30°C)
     .-.
  ― (   ) ―  Humidity: 42%
     `-'
    /   \     Wind: 32km/h ↙ (SW)
```

## Features

- **No API key required** — uses the free Open-Meteo API
- **Color-coded output** — temperature and wind speed are colorized by severity
- **ASCII weather icons** — visual representation of current conditions
- **Cross-platform** — works on Windows, macOS, and Linux

## Requirements

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) or later

## Installation

### 1. Install the .NET 10 SDK

#### Windows

```powershell
winget install Microsoft.DotNet.SDK.10
```

Or download from https://dotnet.microsoft.com/download

#### macOS

```bash
brew install --cask dotnet-sdk
```

Or download from https://dotnet.microsoft.com/download

#### Linux

```bash
# Ubuntu / Debian
sudo apt-get update && sudo apt-get install -y dotnet-sdk-10.0

# Or use the install script:
curl -sSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin --channel 10.0
```

See https://dotnet.microsoft.com/download for distro-specific instructions.

### 2. Clone and install Weathery

```bash
git clone https://github.com/Graveweaver/Weathery.git
cd Weathery
dotnet pack -c Release
dotnet tool install -g weathery --add-source ./bin/Release
```

**macOS / Linux users:** The .NET tools directory may not be on your PATH by default. Add this to your shell profile:

**macOS (zsh):**
```bash
echo 'export PATH="$HOME/.dotnet/tools:$PATH"' >> ~/.zshrc
source ~/.zshrc
```

**Linux (bash):**
```bash
echo 'export PATH="$HOME/.dotnet/tools:$PATH"' >> ~/.bashrc
source ~/.bashrc
```

### 3. Try it

```bash
weathery -city budapest
```

## Usage

```bash
weathery -city <city-name>
```

Examples:

```bash
weathery -city london
weathery -city "san francisco"
weathery -city tokyo
```

**Note:** The city name must match the geocoding result exactly (case-insensitive). If the city isn't found, try a more specific name.

## Updating

```bash
cd Weathery
git pull
dotnet pack -c Release
dotnet tool update -g weathery --add-source ./bin/Release
```

## Uninstalling

```bash
dotnet tool uninstall -g weathery
```

## How it works

1. Takes a city name from the command line
2. Calls Open-Meteo's [Geocoding API](https://open-meteo.com/en/docs/geocoding-api) to convert the name to latitude/longitude
3. Calls Open-Meteo's [Forecast API](https://open-meteo.com/en/docs) for current weather conditions
4. Renders the result with ASCII art icons and color-coded statistics

**Weather data provided by [Open-Meteo.com](https://open-meteo.com).**

## License

MIT — see [LICENSE](LICENSE).
