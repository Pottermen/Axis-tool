using AxisConfigurator.Core.Domain;

namespace AxisConfigurator.Core.Interfaces;

/// <summary>
/// Client for communicating with Axis cameras via VAPIX API
/// </summary>
public interface IAxisApiClient
{
    /// <summary>
    /// Gets camera information
    /// </summary>
    /// <param name="ipAddress">Camera IP address</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Camera information</returns>
    Task<CameraRecord?> GetCameraInfoAsync(string ipAddress, CancellationToken cancellationToken = default);

    /// <summary>
    /// Configures camera with new IP address
    /// </summary>
    /// <param name="camera">Camera to configure</param>
    /// <param name="newIpAddress">New IP address</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if configuration was successful</returns>
    Task<bool> ConfigureCameraAsync(CameraRecord camera, string newIpAddress, CancellationToken cancellationToken = default);

    /// <summary>
    /// Tests connectivity to a camera
    /// </summary>
    /// <param name="ipAddress">Camera IP address</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if camera is reachable</returns>
    Task<bool> TestConnectivityAsync(string ipAddress, CancellationToken cancellationToken = default);
}