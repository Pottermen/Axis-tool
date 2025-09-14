namespace AxisConfigurator.Core.Domain;

/// <summary>
/// Represents the current state of a camera during provisioning
/// </summary>
public enum CameraState
{
    /// <summary>
    /// Camera was discovered but not yet configured
    /// </summary>
    Discovered,

    /// <summary>
    /// Camera configuration is in progress
    /// </summary>
    Configuring,

    /// <summary>
    /// Camera has been successfully configured
    /// </summary>
    Configured,

    /// <summary>
    /// Camera configuration failed
    /// </summary>
    Failed,

    /// <summary>
    /// Camera configuration was skipped
    /// </summary>
    Skipped
}