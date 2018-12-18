using System.Windows.Media;

namespace WpfApp1
{
    public interface IMainWindowViewModel
    {
        IUserControlViewModel UserControlViewModel { get; set; }
    }

    public interface IUserControlViewModel
    {
        int TotalCount { get; }
        int ErrorCount { get; set; }
        int PendingCount { get; set; }
        int SuccessCount { get; set; }
        PointCollection PendingPoints { get; }
        double Size { get; set; }
        PointCollection ErrorPoints { get; }
        PointCollection SuccessPoints { get; }
    }
}