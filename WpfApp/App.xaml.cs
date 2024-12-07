using System.Windows;
using Unity;

namespace WpfApp
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        public static IUnityContainer Container { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Container = DependencyInjectionConfig.RegisterDependencies();
            MainWindow mainWindow = Container.Resolve<MainWindow>();
            mainWindow.Show();
        }
    }
}
