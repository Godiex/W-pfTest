using System.Windows;
using WpfApp.ViewModels;
using WpfApp.Views.Areas;
using WpfApp.Views.Users;

namespace WpfApp
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly UserManagementViewModel _userManagementViewModel;
        private readonly AreaManagementViewModel _areaManagementViewModel;
        public MainWindow(
            UserManagementViewModel userManagementViewModel,
            AreaManagementViewModel areaManagementViewModel
        )
        {
            InitializeComponent();
            _userManagementViewModel = userManagementViewModel;
            _areaManagementViewModel = areaManagementViewModel;
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
                case "AreaManagementView":
                    AreaManagementView areaManagementView = new AreaManagementView(MainFrame)
                    {
                        DataContext = _areaManagementViewModel
                    };
                    MainFrame.Tag = areaManagementView;
                    MainFrame.Content = areaManagementView;
                    break;
                default:
                    MessageBox.Show("Vista no reconocida.");
                    break;
            }
        }
    }
}
