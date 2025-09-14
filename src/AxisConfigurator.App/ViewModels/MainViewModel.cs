using AxisConfigurator.App.Helpers;
using AxisConfigurator.Core.Domain;
using AxisConfigurator.Core.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Threading;

namespace AxisConfigurator.App.ViewModels;

/// <summary>
/// Main view model for the application
/// </summary>
public class MainViewModel : INotifyPropertyChanged
{
    private readonly IDiscoveryService _discoveryService;
    private readonly IAxisApiClient _axisApiClient;
    private readonly IIpAllocationService _ipAllocationService;
    private readonly ILogger<MainViewModel> _logger;
    private readonly DispatcherTimer _discoveryTimer;

    private string _selectedMode = "Automatic";
    private string _statusText = "Ready";
    private CameraViewModel? _selectedCamera;
    private bool _isDiscoveryRunning;

    public MainViewModel(
        IDiscoveryService discoveryService,
        IAxisApiClient axisApiClient,
        IIpAllocationService ipAllocationService,
        ILogger<MainViewModel> logger)
    {
        _discoveryService = discoveryService;
        _axisApiClient = axisApiClient;
        _ipAllocationService = ipAllocationService;
        _logger = logger;

        Cameras = new ObservableCollection<CameraViewModel>();
        AvailableModes = new[] { "Automatic", "Manual" };

        // Initialize commands
        StartCommand = new RelayCommand(_ => StartDiscovery(), _ => !_isDiscoveryRunning);
        StopCommand = new RelayCommand(_ => StopDiscovery(), _ => _isDiscoveryRunning);
        ConfigureCommand = new RelayCommand(_ => ConfigureSelectedCamera(), _ => SelectedCamera != null);

        // Setup discovery timer (10 second interval)
        _discoveryTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(10)
        };
        _discoveryTimer.Tick += OnDiscoveryTimer;

        _logger.LogInformation("MainViewModel initialized");
    }

    public ObservableCollection<CameraViewModel> Cameras { get; }
    public string[] AvailableModes { get; }

    public string SelectedMode
    {
        get => _selectedMode;
        set
        {
            if (_selectedMode != value)
            {
                _selectedMode = value;
                OnPropertyChanged();
                _logger.LogInformation("Mode changed to {Mode}", value);
            }
        }
    }

    public string StatusText
    {
        get => _statusText;
        set
        {
            if (_statusText != value)
            {
                _statusText = value;
                OnPropertyChanged();
            }
        }
    }

    public CameraViewModel? SelectedCamera
    {
        get => _selectedCamera;
        set
        {
            if (_selectedCamera != value)
            {
                _selectedCamera = value;
                OnPropertyChanged();
            }
        }
    }

    public ICommand StartCommand { get; }
    public ICommand StopCommand { get; }
    public ICommand ConfigureCommand { get; }

    private async void StartDiscovery()
    {
        try
        {
            _isDiscoveryRunning = true;
            StatusText = "Starting discovery...";
            _logger.LogInformation("Starting camera discovery");

            _discoveryTimer.Start();
            await PerformDiscovery();

            StatusText = "Discovery running";
            OnPropertyChanged(nameof(StartCommand));
            OnPropertyChanged(nameof(StopCommand));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error starting discovery");
            StatusText = $"Error starting discovery: {ex.Message}";
            _isDiscoveryRunning = false;
        }
    }

    private void StopDiscovery()
    {
        try
        {
            _discoveryTimer.Stop();
            _isDiscoveryRunning = false;
            StatusText = "Discovery stopped";
            _logger.LogInformation("Camera discovery stopped");

            OnPropertyChanged(nameof(StartCommand));
            OnPropertyChanged(nameof(StopCommand));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error stopping discovery");
            StatusText = $"Error stopping discovery: {ex.Message}";
        }
    }

    private async void OnDiscoveryTimer(object? sender, EventArgs e)
    {
        await PerformDiscovery();
    }

    private async Task PerformDiscovery()
    {
        try
        {
            var discoveredCameras = await _discoveryService.DiscoverCamerasAsync();
            
            foreach (var camera in discoveredCameras)
            {
                var existingCamera = Cameras.FirstOrDefault(c => c.MacAddress == camera.MacAddress);
                if (existingCamera == null)
                {
                    // New camera discovered
                    var cameraViewModel = new CameraViewModel(camera);
                    
                    // Auto-assign IP in automatic mode
                    if (SelectedMode == "Automatic")
                    {
                        var nextIp = _ipAllocationService.GetNextAvailableIp();
                        if (nextIp != null)
                        {
                            cameraViewModel.NewIpAddress = nextIp;
                            _ipAllocationService.ReserveIp(nextIp);
                        }
                    }
                    
                    Cameras.Add(cameraViewModel);
                    _logger.LogDebug("Added new camera {MacAddress}", camera.MacAddress);
                }
                else
                {
                    // Update existing camera
                    existingCamera.CurrentIpAddress = camera.CurrentIpAddress;
                    existingCamera.Model = camera.Model;
                    existingCamera.FirmwareVersion = camera.FirmwareVersion;
                    existingCamera.LastUpdated = camera.LastUpdated;
                }
            }

            StatusText = $"Discovery running - {Cameras.Count} cameras found";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during discovery");
            StatusText = $"Discovery error: {ex.Message}";
        }
    }

    private async void ConfigureSelectedCamera()
    {
        if (SelectedCamera?.NewIpAddress == null)
        {
            StatusText = "Please assign a new IP address to the selected camera";
            return;
        }

        try
        {
            SelectedCamera.State = CameraState.Configuring;
            StatusText = $"Configuring camera {SelectedCamera.MacAddress}...";
            _logger.LogInformation("Configuring camera {MacAddress} to IP {NewIp}", 
                SelectedCamera.MacAddress, SelectedCamera.NewIpAddress);

            var success = await _axisApiClient.ConfigureCameraAsync(
                SelectedCamera.CameraRecord, 
                SelectedCamera.NewIpAddress);

            if (success)
            {
                SelectedCamera.State = CameraState.Configured;
                SelectedCamera.CurrentIpAddress = SelectedCamera.NewIpAddress;
                StatusText = $"Camera {SelectedCamera.MacAddress} configured successfully";
                _logger.LogInformation("Camera {MacAddress} configured successfully", SelectedCamera.MacAddress);
            }
            else
            {
                SelectedCamera.State = CameraState.Failed;
                SelectedCamera.ErrorMessage = "Configuration failed";
                StatusText = $"Failed to configure camera {SelectedCamera.MacAddress}";
                _logger.LogWarning("Failed to configure camera {MacAddress}", SelectedCamera.MacAddress);
            }

            SelectedCamera.LastUpdated = DateTime.UtcNow;
        }
        catch (Exception ex)
        {
            SelectedCamera.State = CameraState.Failed;
            SelectedCamera.ErrorMessage = ex.Message;
            StatusText = $"Error configuring camera: {ex.Message}";
            _logger.LogError(ex, "Error configuring camera {MacAddress}", SelectedCamera.MacAddress);
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}