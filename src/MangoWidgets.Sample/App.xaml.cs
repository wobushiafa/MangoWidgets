using MangoWidgets.Interfaces;
using MangoWidgets.MVVM.Contracts;
using MangoWidgets.MVVM.Service;
using MangoWidgets.Sample.ViewModels;
using MangoWidgets.Sample.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;

namespace MangoWidgets.Sample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly IHost _host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) => 
            {
                services.AddHostedService<ApplicationHostService>();
                services.AddSingleton<IDialogService, DialogService>();

                services.AddTransient<MainView>();
                services.AddTransient<MainViewModel>();

                services.AddTransient<IDialogControl<TestDialogViewModel>, TestDialogView>();
                services.AddTransient<TestDialogViewModel>();
            })
            .Build();

        public static T Resolve<T>() where T : class
        {
            var obj = _host.Services.GetService<T>();
            if (obj is null)
                throw new NullReferenceException();
            return obj;
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();
            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync();
            base.OnExit(e);
        }
    }
}