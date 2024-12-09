using System.Windows;
using System.Windows.Controls;
using Application.UseCase.GetLast10UsersHandler;

namespace WpfApp
{
    public partial class UserManagementView : UserControl
    {
        private readonly Frame _mainFrame;

        public UserManagementView(Frame mainFrame)
        {
            InitializeComponent();
            _mainFrame = mainFrame;
        }

        private void AddUserButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_mainFrame != null)
            {
                _mainFrame.Content = new UserFormView(_mainFrame)
                {
                    DataContext = this.DataContext
                };
            }
        }

        private void EditUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (_mainFrame != null && sender is Button button && button.CommandParameter is UserDto selectedUser)
            {
                _mainFrame.Content = new UserEditFormView(_mainFrame, selectedUser)
                {
                    DataContext = this.DataContext
                };
            }
        }
    }
    
}