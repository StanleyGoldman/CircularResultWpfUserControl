using System.Reactive.Disposables;
using System.Windows;
using ReactiveUI;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IViewFor<IMainWindowViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel", typeof(IMainWindowViewModel), typeof(MainWindow), new PropertyMetadata(null));

        public MainWindow()
        {
            InitializeComponent();

            this.WhenActivated(disposable =>
                {
                    this.OneWayBind(ViewModel,
                            model => model.UserControlViewModel,
                            window => window.CircularDisplay.ViewModel)
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
