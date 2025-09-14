# Axis Camera Configuration Tool

A Windows WPF application for automatically and manually provisioning Axis network cameras with IP configurations.

## Overview

The Axis Camera Configuration Tool simplifies the process of configuring multiple Axis network cameras by automating IP address assignment and device provisioning. The application can discover cameras on the network and configure them with new IP addresses either automatically or through manual intervention.

## Features

- **Automatic Discovery**: Discovers Axis cameras on the local network
- **IP Address Management**: Automatic and manual IP address allocation
- **Configuration Modes**: 
  - Automatic: Assigns IP addresses automatically from a configured range
  - Manual: Allows manual IP assignment for each camera
- **Real-time Status**: Live updates on camera configuration progress
- **Logging**: Comprehensive logging with file rotation
- **Modern UI**: Clean WPF interface with data binding

## Quick Start

### Prerequisites

- Windows 10/11
- .NET 8.0 Runtime
- Network access to Axis cameras

### Installation

1. Download the latest release from the [Releases page](../../releases)
2. Run the MSI installer
3. Launch "Axis Camera Configuration Tool" from the Start Menu

### Basic Usage

1. **Select Mode**: Choose between "Automatic" or "Manual" configuration mode
2. **Start Discovery**: Click "Start Discovery" to scan for cameras
3. **Configure Cameras**: 
   - **Automatic**: Cameras will be assigned IPs automatically
   - **Manual**: Select a camera and assign an IP manually
4. **Monitor Progress**: Watch the status updates in the camera list

## Project Structure

```
├── src/
│   ├── AxisConfigurator.sln           # Solution file
│   ├── AxisConfigurator.Core/         # Core domain logic and services
│   │   ├── Domain/                    # Domain models (CameraRecord, CameraState)
│   │   ├── Interfaces/                # Service interfaces
│   │   ├── Services/                  # Service implementations
│   │   └── Logging/                   # Logging configuration
│   └── AxisConfigurator.App/          # WPF Application
│       ├── ViewModels/                # MVVM ViewModels
│       ├── Helpers/                   # UI helpers (RelayCommand)
│       ├── MainWindow.xaml            # Main UI window
│       └── App.xaml                   # Application bootstrap
├── tests/
│   └── AxisConfigurator.Tests/        # Unit tests
├── build/
│   └── wix/                          # WiX installer configuration
├── docs/                             # Documentation
├── .github/                          # GitHub workflows and templates
└── logs/                             # Application logs (created at runtime)
```

## Configuration

The application uses `appsettings.json` for configuration:

```json
{
  "Provisioning": {
    "ScanIntervalSeconds": 10,
    "MaxConcurrentConfig": 3,
    "IpRange": {
      "BaseAddress": "192.168.1",
      "StartRange": 10,
      "EndRange": 99
    }
  }
}
```

## Development

### Building from Source

```bash
# Clone the repository
git clone https://github.com/Pottermen/Axis-tool.git
cd Axis-tool

# Restore dependencies
dotnet restore src/AxisConfigurator.sln

# Build the solution
dotnet build src/AxisConfigurator.sln --configuration Release

# Run tests
dotnet test tests/AxisConfigurator.Tests/
```

### Architecture

The application follows a clean architecture pattern:

- **Core Layer**: Domain models and business logic
- **Application Layer**: WPF UI with MVVM pattern
- **Infrastructure Layer**: External services (network discovery, camera APIs)

## Contributing

Please read [CONTRIBUTING.md](CONTRIBUTING.md) for details on our code of conduct and the process for submitting pull requests.

## Roadmap

See [docs/roadmap.md](docs/roadmap.md) for planned features and milestones.

## Known Limitations

**Current Version (0.1.0)**:
- Discovery uses stub implementation (no real network scanning)
- Camera API uses stub implementation (no real VAPIX integration)
- Manual IP conflict detection only
- Basic retry logic

## Support

- **Issues**: [GitHub Issues](../../issues)
- **Documentation**: [docs/](docs/)
- **Contributing**: [CONTRIBUTING.md](CONTRIBUTING.md)

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Next Steps

This is the initial scaffold release. Upcoming features include:

- Real network discovery implementation
- Axis VAPIX API integration
- Advanced provisioning state machine
- MSI packaging finalization
- Security hardening
- Simulation harness for testing

For detailed planned work, see [docs/roadmap.md](docs/roadmap.md).