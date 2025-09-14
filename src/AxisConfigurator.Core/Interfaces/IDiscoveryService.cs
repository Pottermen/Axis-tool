using AxisConfigurator.Core.Domain;

namespace AxisConfigurator.Core.Interfaces;

/// <summary>
/// Service for discovering Axis cameras on the network
/// </summary>
public interface IDiscoveryService
{
    /// <summary>
    /// Discovers cameras on the network
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Collection of discovered cameras</returns>
    Task<IEnumerable<CameraRecord>> DiscoverCamerasAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Event raised when cameras are discovered
    /// </summary>
    event EventHandler<IEnumerable<CameraRecord>>? CamerasDiscovered;
}