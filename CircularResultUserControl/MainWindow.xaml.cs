using System.Reactive.Disposables;
using System.Windows;
using ReactiveUI;

namespace CircularResultUserControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IViewFor<IMainWindowViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel", typeof(IMainWindowViewModel), typeof(MainWindow),
            new PropertyMetadata(null, (d, args) => ((MainWindow)d).ViewModel = (IMainWindowViewModel)args.NewValue));

        public MainWindow()
        {
            InitializeComponent();

            this.WhenActivated(disposable =>
                {
                    this.OneWayBind(ViewModel,
                            model => model.UserControlViewModel.ErrorCount,
                            window => window.CircularDisplay.ErrorCount)
                        .DisposeWith(disposable);

                    this.OneWayBind(ViewModel,
                            model => model.UserControlViewModel.SuccessCount,
                            window => window.CircularDisplay.SuccessCount)
                        .DisposeWith(disposable);

                    this.OneWayBind(ViewModel,
                            model => model.UserControlViewModel.PendingCount,
                            window => window.CircularDisplay.PendingCount)
                        .DisposeWith(disposable);
                }
            );
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (IMainWindowViewModel)value;
        }

        public IMainWindowViewModel ViewModel
        {
            get => (IMainWindowViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }
    }
}
