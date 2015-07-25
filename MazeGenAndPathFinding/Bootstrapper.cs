using MazeGenAndPathFinding.Views;
using Microsoft.Practices.Prism.UnityExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
        }
    }
}
