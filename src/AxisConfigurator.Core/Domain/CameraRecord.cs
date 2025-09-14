namespace AxisConfigurator.Core.Domain;

/// <summary>
/// Represents a camera record with its current information
/// </summary>
public class CameraRecord
{
    /// <summary>
    /// MAC address of the camera (unique identifier)
    /// </summary>
    public string MacAddress { get; set; } = string.Empty;

    /// <summary>
    /// Current IP address of the camera
    /// </summary>
    public string? CurrentIpAddress { get; set; }

    /// <summary>
    /// New IP address to assign to the camera
    /// </summary>
    public string? NewIpAddress { get; set; }

    /// <summary>
    /// Current state of the camera
    /// </summary>
    public CameraState State { get; set; } = CameraState.Discovered;

    /// <summary>
    /// Camera model information
    /// </summary>
    public string? Model { get; set; }

    /// <summary>
    /// Firmware version
    /// </summary>
    public string? FirmwareVersion { get; set; }

    /// <summary>
    /// When this record was last updated
    /// </summary>
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Error message if configuration failed
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Number of configuration attempts
    /// </summary>
    public int AttemptCount { get; set; } = 0;
}