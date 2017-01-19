using System.Windows;
using MazeGenAndPathFinding.Views;

namespace MazeGenAndPathFinding
{
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainWindow = new MainWindow();
            MainWindow.Show();
        }
    }
}
