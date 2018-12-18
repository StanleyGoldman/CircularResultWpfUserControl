using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace WpfApp1
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
            userControlViewModel.Size = 250;
            userControlViewModel.TotalCount = 30;
            userControlViewModel.PendingCount = 30;
            userControlViewModel.SuccessCount = 5;
            userControlViewModel.ErrorCount = 3;

            var mainWindowViewModel = serviceProvider.GetService<IMainWindowViewModel>();
            mainWindowViewModel.UserControlViewModel = userControlViewModel;

            mainWindow.ViewModel = mainWindowViewModel;
            mainWindow.Show();
        }
    }
}
