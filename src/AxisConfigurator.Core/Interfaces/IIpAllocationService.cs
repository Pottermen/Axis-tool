namespace AxisConfigurator.Core.Interfaces;

/// <summary>
/// Service for allocating IP addresses to cameras
/// </summary>
public interface IIpAllocationService
{
    /// <summary>
    /// Gets the next available IP address in the configured range
    /// </summary>
    /// <returns>Next available IP address or null if none available</returns>
    string? GetNextAvailableIp();

    /// <summary>
    /// Reserves an IP address for use
    /// </summary>
    /// <param name="ipAddress">IP address to reserve</param>
    /// <returns>True if reservation was successful</returns>
    bool ReserveIp(string ipAddress);

    /// <summary>
    /// Releases a reserved IP address
    /// </summary>
    /// <param name="ipAddress">IP address to release</param>
    /// <returns>True if release was successful</returns>
    bool ReleaseIp(string ipAddress);

    /// <summary>
    /// Checks if an IP address is available
    /// </summary>
    /// <param name="ipAddress">IP address to check</param>
    /// <returns>True if IP is available</returns>
    bool IsIpAvailable(string ipAddress);

    /// <summary>
    /// Gets all reserved IP addresses
    /// </summary>
    /// <returns>Collection of reserved IP addresses</returns>
    IEnumerable<string> GetReservedIps();
}