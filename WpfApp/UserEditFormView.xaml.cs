using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Application.UseCase.GetLast10UsersHandler;
using Application.UseCase.UpdateContactData;
using WpfApp.ViewModels;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace WpfApp
{
    public partial class UserEditFormView : UserControl
    {
        private readonly Frame _mainFrame;
        private readonly UserDto _userToEdit;

        public UserEditFormView(Frame mainFrame, UserDto userToEdit)
        {
            InitializeComponent();
            _mainFrame = mainFrame;
            _userToEdit = userToEdit;

            // Rellenar campos con la información del usuario
            LoadUserData();
        }

        private void LoadUserData()
        {
            IdentificationTextBox.Text = _userToEdit.Identification;
            FullNameTextBox.Text = _userToEdit.FullName;
            EmailTextBox.Text = _userToEdit.Email;
            PhoneTextBox.Text = _userToEdit.Phone;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UpdateContactDataCommand contactData = await GetContactDataFromForm();
                if (!(DataContext is UserManagementViewModel viewModel))
                {
                    return;
                }

                await viewModel.UpdateUserAsync(contactData);
                MessageBox.Show("Usuario actualizado con éxito", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                Back();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al actualizar el usuario: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        private async Task<UpdateContactDataCommand> GetContactDataFromForm()
        {
            string email = EmailTextBox.Text;
            string phone = PhoneTextBox.Text;
            UpdateContactDataCommand newUser = new UpdateContactDataCommand(IdentificationTextBox.Text, email, phone);
            UpdateContactDataValidator updateContactDataValidator = new UpdateContactDataValidator();
            ValidationResult result = await updateContactDataValidator.ValidateAsync(newUser);
            
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
            if (_mainFrame?.Tag is UserManagementView userManagementView)
            {
                _mainFrame.Content = userManagementView;
            }
        }

        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^\S+@\S+\.\S+$");
        }
        
        private void ValidateNumbersOnly(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"^\d+$");
        }
    }
}