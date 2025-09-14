using AxisConfigurator.Core.Interfaces;
using Microsoft.Extensions.Logging;
using System.Net;

namespace AxisConfigurator.Core.Services;

/// <summary>
/// Service for allocating IP addresses to cameras
/// TODO: Add real IP conflict detection and network scanning
/// </summary>
public class IpAllocationService : IIpAllocationService
{
    private readonly ILogger<IpAllocationService> _logger;
    private readonly HashSet<string> _reservedIps;
    private readonly string _baseIpAddress;
    private readonly int _startRange;
    private readonly int _endRange;
    private int _currentIndex;

    public IpAllocationService(ILogger<IpAllocationService> logger)
    {
        _logger = logger;
        _reservedIps = new HashSet<string>();
        
        // TODO: Make these configurable via settings
        _baseIpAddress = "192.168.1"; // Base IP for the network
        _startRange = 10; // Start of IP range
        _endRange = 99;   // End of IP range
        _currentIndex = _startRange;

        _logger.LogInformation("IP allocation service initialized with range {BaseIp}.{Start}-{End}", 
            _baseIpAddress, _startRange, _endRange);
    }

    public string? GetNextAvailableIp()
    {
        for (int i = 0; i < (_endRange - _startRange + 1); i++)
        {
            var ipAddress = $"{_baseIpAddress}.{_currentIndex}";
            
            if (IsIpAvailable(ipAddress))
            {
                _logger.LogDebug("Next available IP: {IpAddress}", ipAddress);
                return ipAddress;
            }

            _currentIndex++;
            if (_currentIndex > _endRange)
            {
                _currentIndex = _startRange;
            }
        }

        _logger.LogWarning("No available IP addresses in the configured range");
        return null;
    }

    public bool ReserveIp(string ipAddress)
    {
        if (!IsValidIpInRange(ipAddress))
        {
            _logger.LogWarning("IP address {IpAddress} is not in valid range", ipAddress);
            return false;
        }

        if (_reservedIps.Contains(ipAddress))
        {
            _logger.LogWarning("IP address {IpAddress} is already reserved", ipAddress);
            return false;
        }

        _reservedIps.Add(ipAddress);
        _logger.LogDebug("Reserved IP address {IpAddress}", ipAddress);
        return true;
    }

    public bool ReleaseIp(string ipAddress)
    {
        if (_reservedIps.Remove(ipAddress))
        {
            _logger.LogDebug("Released IP address {IpAddress}", ipAddress);
            return true;
        }

        _logger.LogWarning("IP address {IpAddress} was not reserved", ipAddress);
        return false;
    }

    public bool IsIpAvailable(string ipAddress)
    {
        if (!IsValidIpInRange(ipAddress))
        {
            return false;
        }

        return !_reservedIps.Contains(ipAddress);
    }

    public IEnumerable<string> GetReservedIps()
    {
        return _reservedIps.ToList();
    }

    private bool IsValidIpInRange(string ipAddress)
    {
        if (!IPAddress.TryParse(ipAddress, out var ip))
        {
            return false;
        }

        var parts = ipAddress.Split('.');
        if (parts.Length != 4)
        {
            return false;
        }

        var basePrefix = $"{_baseIpAddress}.";
        if (!ipAddress.StartsWith(basePrefix))
        {
            return false;
        }

        if (int.TryParse(parts[3], out var lastOctet))
        {
            return lastOctet >= _startRange && lastOctet <= _endRange;
        }

        return false;
    }
}