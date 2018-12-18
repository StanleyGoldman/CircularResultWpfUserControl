using System;
using System.Windows.Media;
using ReactiveUI;

namespace WpfApp1
{
    public class MainWindowViewModel : ReactiveObject, IMainWindowViewModel
    {
        private IUserControlViewModel _userControlViewModel;

        public IUserControlViewModel UserControlViewModel
        {
            get => _userControlViewModel;
            set => this.RaiseAndSetIfChanged(ref _userControlViewModel, value);
        }
    }

    public class UserControlViewModel : ReactiveObject, IUserControlViewModel
    {
        private int _errorCount;
        private int _pendingCount;
        private int _successCount;

        public int ErrorCount
        {
            get => _errorCount;
            set => this.RaiseAndSetIfChanged(ref _errorCount, value);
        }

        public int PendingCount
        {
            get => _pendingCount;
            set => this.RaiseAndSetIfChanged(ref _pendingCount, value);
        }

        public int SuccessCount
        {
            get => _successCount;
            set => this.RaiseAndSetIfChanged(ref _successCount, value);
        }
    }
}