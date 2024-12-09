using System.Windows;
using WpfApp.ViewModels;

namespace WpfApp
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly UserManagementViewModel _userManagementViewModel;
        public MainWindow(
            UserManagementViewModel userManagementViewModel
        )
        {
            InitializeComponent();
            _userManagementViewModel = userManagementViewModel;
        }
        
        private void MenuControl_OnNavigate(string viewName)
        {
            switch (viewName)
            {
                case "UserManagementView":
                    UserManagementView userManagementView = new UserManagementView(MainFrame)
                    {
                        DataContext = _userManagementViewModel
                    };
                    MainFrame.Tag = userManagementView;
                    MainFrame.Content = userManagementView;
                    break;
                default:
                    MessageBox.Show("Vista no reconocida.");
                    break;
            }
        }
    }
}
