using MazeGenAndPathFinding.ViewModels;

namespace MazeGenAndPathFinding.Views
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            DataContext = new MainViewModel();
            InitializeComponent();
        }
    }
}
