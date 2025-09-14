using AxisConfigurator.Core.Domain;
using AxisConfigurator.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace AxisConfigurator.Core.Services;

/// <summary>
/// Stub implementation of discovery service for development and testing
/// TODO: Replace with real network discovery implementation using ARP/ICMP
/// </summary>
public class DiscoveryServiceStub : IDiscoveryService
{
    private readonly ILogger<DiscoveryServiceStub> _logger;
    private readonly Random _random = new();

    public DiscoveryServiceStub(ILogger<DiscoveryServiceStub> logger)
    {
        _logger = logger;
    }

    public event EventHandler<IEnumerable<CameraRecord>>? CamerasDiscovered;

    public async Task<IEnumerable<CameraRecord>> DiscoverCamerasAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Starting camera discovery (stub implementation)");

        // Simulate network discovery delay
        await Task.Delay(1000, cancellationToken);

        // Generate some mock cameras for testing
        var cameras = GenerateMockCameras();

        _logger.LogInformation("Discovered {CameraCount} cameras", cameras.Count);
        CamerasDiscovered?.Invoke(this, cameras);

        return cameras;
    }

    private List<CameraRecord> GenerateMockCameras()
    {
        var cameras = new List<CameraRecord>();
        
        // Generate 2-5 mock cameras
        var cameraCount = _random.Next(2, 6);
        
        for (int i = 0; i < cameraCount; i++)
        {
            cameras.Add(new CameraRecord
            {
                MacAddress = $"AC:CC:8E:{_random.Next(10, 99):X2}:{_random.Next(10, 99):X2}:{_random.Next(10, 99):X2}",
                CurrentIpAddress = $"192.168.1.{_random.Next(100, 200)}",
                Model = $"AXIS P{_random.Next(1000, 9999)}",
                FirmwareVersion = $"9.{_random.Next(1, 9)}.{_random.Next(1, 99)}",
                State = CameraState.Discovered,
                LastUpdated = DateTime.UtcNow
            });
        }

        return cameras;
    }
}