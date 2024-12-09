using System;
using System.Windows;
using System.Windows.Controls;
using Infrastructure.Extensions;
using Unity;

namespace WpfApp
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        private static IUnityContainer Container { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Container = DependencyInjectionConfig.RegisterDependencies();
            MainWindow mainWindow = Container.Resolve<MainWindow>();
            mainWindow.Show();
        }
    }
}
