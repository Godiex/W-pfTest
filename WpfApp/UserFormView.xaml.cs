using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Application.UseCase.CreateUser;
using WpfApp.ViewModels;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace WpfApp
{
    public partial class UserFormView : UserControl
    {
        private readonly Frame _mainFrame;

        public UserFormView(Frame mainFrame)
        {
            InitializeComponent();
            _mainFrame = mainFrame;
        }
        
        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CreateUserCommand newUser = await GetUserFromForm();
                if (!(DataContext is UserManagementViewModel viewModel))
                {
                    return;
                }
                await viewModel.CreateUserAsync(newUser);
                MessageBox.Show("Usuario registrado con éxito", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                Back();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al registrar el usuario: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        
        }
        
        private async Task<CreateUserCommand> GetUserFromForm()
        {
            string identification = IdentificationTextBox.Text;
            string fullName = FullNameTextBox.Text;
            string email = EmailTextBox.Text;
            string phone = PhoneTextBox.Text;
            CreateUserCommand newUser = new CreateUserCommand(identification, fullName, email, phone);
            CreateUserValidator createUserValidator = new CreateUserValidator();
            ValidationResult result = await createUserValidator.ValidateAsync(newUser);
            
            if (!result.IsValid)
            {
                throw new Exception(ValidationUtils.ValidateResult(result));
            }
            
            return newUser;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Back();
        }

        private void Back()
        {
            if (!(_mainFrame?.Content is UserFormView)) return;

            if (_mainFrame.Tag is UserManagementView userManagementView)
            {
                _mainFrame.Content = userManagementView;
            }
        }
        
        private void ValidateTextOnly(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"^[a-zA-Z\s]+$");
        }
        
        private void ValidateNumbersOnly(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"^\d+$");
        }
    }
}