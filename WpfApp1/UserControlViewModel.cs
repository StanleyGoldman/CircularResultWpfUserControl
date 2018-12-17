using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using ReactiveUI;

namespace WpfApp1
{
    public static class UserControlFunctions
    {
        public static IEnumerable<Point> GeneratePoints(double height, double width, float percentage)
        {
            if (percentage < 0 || percentage > 1)
            {
                throw new ArgumentException();
            }

            var degrees = ((percentage / 360) + 90) % 360;

            if (degrees < 90)
            {
                return new[] { new Point(25, 25), new Point(200, 100), new Point(200, 200), new Point(300, 30) };
            }

            if (degrees >= 90 && degrees < 180)
            {
                return new[] { new Point(25, 25), new Point(200, 100), new Point(200, 200), new Point(300, 30) };
            }

            if (degrees >= 180 && degrees < 270)
            {
                return new[] { new Point(25, 25), new Point(200, 100), new Point(200, 200), new Point(300, 30) };
            }

            if (degrees >= 270 && degrees < 360)
            {
                return new[] { new Point(25, 25), new Point(200, 100), new Point(200, 200), new Point(300, 30) };
            }

            throw new InvalidOperationException();
        }
    }

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
        private int _totalCount;
        private int _errorCount;
        private int _pendingCount;
        private int _successCount;
        private double _height;
        private double _width;

        public UserControlViewModel()
        {
            pendingPoints = this.WhenAnyValue(
                    model => model.PendingCount, 
                    model => model.TotalCount, 
                    (pendingCount, totalCount) => CalculateMaskPoints((float)pendingCount / totalCount))
                .ToProperty(this, x => x.PendingPoints);
        }

        private PointCollection CalculateMaskPoints(float percentage)
        {
            try
            {
                var generatePoints = UserControlFunctions.GeneratePoints(Height, Width, percentage);
                return new PointCollection(generatePoints);
            }
            catch (Exception ex)
            {
                return new PointCollection();
            }
        }

        public int TotalCount
        {
            get => _totalCount;
            set => this.RaiseAndSetIfChanged(ref _totalCount, value);
        }

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

        public double Height
        {
            get => _height;
            set => this.RaiseAndSetIfChanged(ref _height, value);
        }

        public double Width
        {
            get => _width;
            set => this.RaiseAndSetIfChanged(ref _width, value);
        }

        readonly ObservableAsPropertyHelper<PointCollection> pendingPoints;
        public PointCollection PendingPoints => pendingPoints.Value;
    }
}