﻿using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace CircularResultUserControl
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var serviceCollection = new ServiceCollection()
                .AddScoped<IUserControlViewModel, UserControlViewModel>()
                .AddScoped<IMainWindowViewModel, MainWindowViewModel>()
                .AddScoped<MainWindow>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = serviceProvider.GetService<MainWindow>();

            var userControlViewModel = serviceProvider.GetService<IUserControlViewModel>();
            userControlViewModel.PendingCount = 3;
            userControlViewModel.SuccessCount = 2;
            userControlViewModel.ErrorCount = 4;

            var mainWindowViewModel = serviceProvider.GetService<IMainWindowViewModel>();
            mainWindowViewModel.UserControlViewModel = userControlViewModel;

            mainWindow.ViewModel = mainWindowViewModel;
            mainWindow.Show();
        }
    }
}
