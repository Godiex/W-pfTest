using System.Windows;
using System.Windows.Controls;
using WpfApp.ViewModels;

namespace WpfApp.Views.Areas
{
    public partial class AreaManagementView : UserControl
    {
        private readonly Frame _mainFrame;

        public AreaManagementView(Frame mainFrame)
        {
            InitializeComponent();
            Loaded += UserManagementView_Loaded;
            _mainFrame = mainFrame;
        }
        
        private async void UserManagementView_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is AreaManagementViewModel viewModel)
            {
                await viewModel.LoadUsersAsync();
            }
        }
    }
}