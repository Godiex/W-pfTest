using System;
using System.Windows.Controls;

namespace WpfApp
{
    public partial class MenuControl : UserControl
    {
        public event Action<string> OnNavigate;

        public MenuControl()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (sender is Button button && button.CommandParameter is string viewName)
            {
                OnNavigate?.Invoke(viewName);
            }
        }
    }
}