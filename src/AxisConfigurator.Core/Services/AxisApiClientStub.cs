using AxisConfigurator.Core.Domain;
using AxisConfigurator.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace AxisConfigurator.Core.Services;

/// <summary>
/// Stub implementation of Axis API client for development and testing
/// TODO: Replace with real VAPIX API implementation
/// </summary>
public class AxisApiClientStub : IAxisApiClient
{
    private readonly ILogger<AxisApiClientStub> _logger;
    private readonly Random _random = new();

    public AxisApiClientStub(ILogger<AxisApiClientStub> logger)
    {
        _logger = logger;
    }

    public async Task<CameraRecord?> GetCameraInfoAsync(string ipAddress, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting camera info for {IpAddress} (stub implementation)", ipAddress);

        // Simulate API call delay
        await Task.Delay(500, cancellationToken);

        // Simulate occasional failures
        if (_random.NextDouble() < 0.1) // 10% failure rate
        {
            _logger.LogWarning("Failed to get camera info for {IpAddress}", ipAddress);
            return null;
        }

        return new CameraRecord
        {
            MacAddress = $"AC:CC:8E:{_random.Next(10, 99):X2}:{_random.Next(10, 99):X2}:{_random.Next(10, 99):X2}",
            CurrentIpAddress = ipAddress,
            Model = $"AXIS P{_random.Next(1000, 9999)}",
            FirmwareVersion = $"9.{_random.Next(1, 9)}.{_random.Next(1, 99)}",
            State = CameraState.Discovered,
            LastUpdated = DateTime.UtcNow
        };
    }

    public async Task<bool> ConfigureCameraAsync(CameraRecord camera, string newIpAddress, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Configuring camera {MacAddress} from {OldIp} to {NewIp} (stub implementation)", 
            camera.MacAddress, camera.CurrentIpAddress, newIpAddress);

        // Simulate configuration delay
        await Task.Delay(2000, cancellationToken);

        // Simulate occasional failures
        if (_random.NextDouble() < 0.15) // 15% failure rate
        {
            _logger.LogWarning("Failed to configure camera {MacAddress}", camera.MacAddress);
            return false;
        }

        _logger.LogInformation("Successfully configured camera {MacAddress}", camera.MacAddress);
        return true;
    }

    public async Task<bool> TestConnectivityAsync(string ipAddress, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Testing connectivity to {IpAddress} (stub implementation)", ipAddress);

        // Simulate ping delay
        await Task.Delay(100, cancellationToken);

        // Simulate occasional connectivity issues
        return _random.NextDouble() > 0.05; // 95% success rate
    }
}