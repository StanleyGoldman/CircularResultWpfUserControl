using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ReactiveUI;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for CircularResult.xaml
    /// </summary>
    public partial class CircularResult : UserControl, IViewFor<IUserControlViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel", typeof(IUserControlViewModel), typeof(CircularResult), new PropertyMetadata(null));

        public CircularResult()
        {
            InitializeComponent();

            this.WhenActivated(disposable =>
                {
                    this.OneWayBind(ViewModel,
                            model => model.PendingPoints,
                            window => window.PendingPolygon.Points)
                        .DisposeWith(disposable);

                    this.OneWayBind(ViewModel,
                            model => model.ErrorPoints,
                            window => window.ErrorPolygon.Points)
                        .DisposeWith(disposable);

                    this.OneWayBind(ViewModel,
                            model => model.SuccessPoints,
                            window => window.SuccessPolygon.Points)
                        .DisposeWith(disposable);

                    this.OneWayBind(ViewModel,
                            model => model.PendingPoints,
                            window => window.PendingPoints.Text,
                            collection => collection.ToString())
                        .DisposeWith(disposable);

                    this.OneWayBind(ViewModel,
                            model => model.ErrorPoints,
                            window => window.ErrorPoints.Text,
                            collection => collection.ToString())
                        .DisposeWith(disposable);

                    this.OneWayBind(ViewModel,
                            model => model.SuccessPoints,
                            window => window.SuccessPoints.Text,
                            collection => collection.ToString())
                        .DisposeWith(disposable);

                    this.Bind(ViewModel,
                            model => model.PendingCount,
                            window => window.PendingCount.Text)
                        .DisposeWith(disposable);

                    this.Bind(ViewModel,
                            model => model.ErrorCount,
                            window => window.ErrorCount.Text)
                        .DisposeWith(disposable);

                    this.Bind(ViewModel,
                            model => model.SuccessCount,
                            window => window.SuccessCount.Text)
                        .DisposeWith(disposable);
                }
            );
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (IUserControlViewModel)value;
        }

        public IUserControlViewModel ViewModel
        {
            get => (IUserControlViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }
    }
}
