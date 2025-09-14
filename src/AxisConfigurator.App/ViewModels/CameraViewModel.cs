using AxisConfigurator.Core.Domain;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AxisConfigurator.App.ViewModels;

/// <summary>
/// ViewModel for a camera record
/// </summary>
public class CameraViewModel : INotifyPropertyChanged
{
    private CameraRecord _cameraRecord;

    public CameraViewModel(CameraRecord cameraRecord)
    {
        _cameraRecord = cameraRecord;
    }

    public string MacAddress
    {
        get => _cameraRecord.MacAddress;
        set
        {
            if (_cameraRecord.MacAddress != value)
            {
                _cameraRecord.MacAddress = value;
                OnPropertyChanged();
            }
        }
    }

    public string? CurrentIpAddress
    {
        get => _cameraRecord.CurrentIpAddress;
        set
        {
            if (_cameraRecord.CurrentIpAddress != value)
            {
                _cameraRecord.CurrentIpAddress = value;
                OnPropertyChanged();
            }
        }
    }

    public string? NewIpAddress
    {
        get => _cameraRecord.NewIpAddress;
        set
        {
            if (_cameraRecord.NewIpAddress != value)
            {
                _cameraRecord.NewIpAddress = value;
                OnPropertyChanged();
            }
        }
    }

    public CameraState State
    {
        get => _cameraRecord.State;
        set
        {
            if (_cameraRecord.State != value)
            {
                _cameraRecord.State = value;
                OnPropertyChanged();
            }
        }
    }

    public string? Model
    {
        get => _cameraRecord.Model;
        set
        {
            if (_cameraRecord.Model != value)
            {
                _cameraRecord.Model = value;
                OnPropertyChanged();
            }
        }
    }

    public string? FirmwareVersion
    {
        get => _cameraRecord.FirmwareVersion;
        set
        {
            if (_cameraRecord.FirmwareVersion != value)
            {
                _cameraRecord.FirmwareVersion = value;
                OnPropertyChanged();
            }
        }
    }

    public DateTime LastUpdated
    {
        get => _cameraRecord.LastUpdated;
        set
        {
            if (_cameraRecord.LastUpdated != value)
            {
                _cameraRecord.LastUpdated = value;
                OnPropertyChanged();
            }
        }
    }

    public string? ErrorMessage
    {
        get => _cameraRecord.ErrorMessage;
        set
        {
            if (_cameraRecord.ErrorMessage != value)
            {
                _cameraRecord.ErrorMessage = value;
                OnPropertyChanged();
            }
        }
    }

    public int AttemptCount
    {
        get => _cameraRecord.AttemptCount;
        set
        {
            if (_cameraRecord.AttemptCount != value)
            {
                _cameraRecord.AttemptCount = value;
                OnPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Gets the underlying camera record
    /// </summary>
    public CameraRecord CameraRecord => _cameraRecord;

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}