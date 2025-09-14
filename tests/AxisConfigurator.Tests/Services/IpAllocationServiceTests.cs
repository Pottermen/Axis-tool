using AxisConfigurator.Core.Services;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;

namespace AxisConfigurator.Tests.Services;

/// <summary>
/// Tests for the IP allocation service
/// </summary>
public class IpAllocationServiceTests
{
    private readonly IpAllocationService _service;

    public IpAllocationServiceTests()
    {
        _service = new IpAllocationService(NullLogger<IpAllocationService>.Instance);
    }

    [Fact]
    public void GetNextAvailableIp_ShouldReturnValidIpInRange()
    {
        // Act
        var ip = _service.GetNextAvailableIp();

        // Assert
        ip.Should().NotBeNull();
        ip.Should().StartWith("192.168.1.");
        var lastOctet = int.Parse(ip!.Split('.')[3]);
        lastOctet.Should().BeInRange(10, 99);
    }

    [Fact]
    public void ReserveIp_WithValidIp_ShouldReturnTrue()
    {
        // Arrange
        var ipToReserve = "192.168.1.50";

        // Act
        var result = _service.ReserveIp(ipToReserve);

        // Assert
        result.Should().BeTrue();
        _service.IsIpAvailable(ipToReserve).Should().BeFalse();
    }

    [Fact]
    public void ReserveIp_WithInvalidIp_ShouldReturnFalse()
    {
        // Arrange
        var invalidIp = "10.0.0.1";

        // Act
        var result = _service.ReserveIp(invalidIp);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void ReserveIp_WithAlreadyReservedIp_ShouldReturnFalse()
    {
        // Arrange
        var ipToReserve = "192.168.1.55";
        _service.ReserveIp(ipToReserve);

        // Act
        var result = _service.ReserveIp(ipToReserve);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void ReleaseIp_WithReservedIp_ShouldReturnTrue()
    {
        // Arrange
        var ipToReserve = "192.168.1.60";
        _service.ReserveIp(ipToReserve);

        // Act
        var result = _service.ReleaseIp(ipToReserve);

        // Assert
        result.Should().BeTrue();
        _service.IsIpAvailable(ipToReserve).Should().BeTrue();
    }

    [Fact]
    public void ReleaseIp_WithNonReservedIp_ShouldReturnFalse()
    {
        // Arrange
        var nonReservedIp = "192.168.1.65";

        // Act
        var result = _service.ReleaseIp(nonReservedIp);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void IsIpAvailable_WithAvailableIp_ShouldReturnTrue()
    {
        // Arrange
        var availableIp = "192.168.1.70";

        // Act
        var result = _service.IsIpAvailable(availableIp);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsIpAvailable_WithReservedIp_ShouldReturnFalse()
    {
        // Arrange
        var ipToReserve = "192.168.1.75";
        _service.ReserveIp(ipToReserve);

        // Act
        var result = _service.IsIpAvailable(ipToReserve);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void GetReservedIps_ShouldReturnAllReservedIps()
    {
        // Arrange
        var ipsToReserve = new[] { "192.168.1.80", "192.168.1.81", "192.168.1.82" };
        foreach (var ip in ipsToReserve)
        {
            _service.ReserveIp(ip);
        }

        // Act
        var reservedIps = _service.GetReservedIps().ToList();

        // Assert
        reservedIps.Should().HaveCount(3);
        reservedIps.Should().Contain(ipsToReserve);
    }

    [Fact]
    public void GetNextAvailableIp_WhenAllIpsReserved_ShouldReturnNull()
    {
        // Arrange - Reserve all IPs in range (10-99)
        for (int i = 10; i <= 99; i++)
        {
            _service.ReserveIp($"192.168.1.{i}");
        }

        // Act
        var ip = _service.GetNextAvailableIp();

        // Assert
        ip.Should().BeNull();
    }
}