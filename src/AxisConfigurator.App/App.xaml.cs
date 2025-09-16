using AxisConfigurator.App.ViewModels;
using AxisConfigurator.Core.Interfaces;
using AxisConfigurator.Core.Logging;
using AxisConfigurator.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace AxisConfigurator.App;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private IHost? _host;

    protected override async void OnStartup(StartupEventArgs e)
    {
        _host = Host.CreateDefaultBuilder()
            .UseSerilogLogging()
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                // Register services
                services.AddSingleton<IDiscoveryService, DiscoveryServiceStub>();
                services.AddSingleton<IAxisApiClient, AxisApiClientStub>();
                services.AddSingleton<IIpAllocationService, IpAllocationService>();

                // Register ViewModels
                services.AddTransient<MainViewModel>();
                services.AddTransient<CameraViewModel>();

                // Register MainWindow
                services.AddSingleton<MainWindow>();
            })
            .Build();

        await _host.StartAsync();

        var mainWindow = _host.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        if (_host != null)
        {
            await _host.StopAsync();
            _host.Dispose();
        }
        base.OnExit(e);
    }
}