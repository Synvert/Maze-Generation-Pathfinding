using MazeGenAndPathFinding.Model;
using MazeGenAndPathFinding.Views;
using Microsoft.Practices.Unity;
using System.Windows;
using Prism.Unity;

namespace MazeGenAndPathFinding
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return new MainWindow();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            
            Container.RegisterInstance<IAlgorithmManager>(new AlgorithmManager());
        }
    }
}
