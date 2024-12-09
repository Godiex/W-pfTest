using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Application.UseCase.AssignUserToArea;
using Application.UseCase.GetAllAreas;
using Application.UseCase.GetLast10UsersHandler;
using GalaSoft.MvvmLight.Command;
using Infrastructure.Extensions;

namespace WpfApp.ViewModels
{
    [ViewModel]
    public class AreaManagementViewModel : ViewModelBase
    {
        private readonly GetAllAreasHandler _getAllAreasHandler;
        private readonly GetLast10UsersHandler _getUsersHandler;
        private readonly AssignUserToAreaHandler _assignUserToAreaHandler;

        public ObservableCollection<AreaDto> Areas { get; set; }
        public ObservableCollection<UserDto> Users { get; set; }

        private AreaDto _selectedArea;
        public AreaDto SelectedArea
        {
            get => _selectedArea;
            set => SetProperty(ref _selectedArea, value);
        }

        private UserDto _selectedUser;
        public UserDto SelectedUser
        {
            get => _selectedUser;
            set => SetProperty(ref _selectedUser, value);
        }

        public ICommand AssignUserToAreaCommand { get; }

        public AreaManagementViewModel(
            GetAllAreasHandler getAllAreasHandler,
            AssignUserToAreaHandler assignUserToAreaHandler,
            GetLast10UsersHandler getUsersHandler
        )
        {
            _getAllAreasHandler = getAllAreasHandler;
            _assignUserToAreaHandler = assignUserToAreaHandler;
            _getUsersHandler = getUsersHandler;
            
            Areas = new ObservableCollection<AreaDto>();
            Users = new ObservableCollection<UserDto>();

            AssignUserToAreaCommand = new RelayCommand(async () => await AssignUserToAreaAsync());

            Task.Run(() => LoadAreasAsync());
            Task.Run(() => LoadUsersAsync());
        }

        public async Task AssignUserToAreaAsync()
        {
            if (SelectedArea == null || SelectedUser == null)
            {
                MessageBox.Show("Por favor selecciona un área y un usuario.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            AssignUserToAreaCommand command = new AssignUserToAreaCommand(SelectedUser.Identification, SelectedArea.Identification);

            try
            {
                await _assignUserToAreaHandler.Handle(command);
                MessageBox.Show($"Usuario '{SelectedUser.FullName}' asignado al área '{SelectedArea.Name}' exitosamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al asignar usuario al área: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        public async Task LoadUsersAsync()
        {
            if (Users == null)
            {
                Users = new ObservableCollection<UserDto>();
            }

            Users.Clear();

            List<UserDto> users = await _getUsersHandler.Handle();
            int counter = 1;
            foreach (UserDto user in users)
            {
                Users.Add(user);
                user.Index = counter++;
            }
        }

        public async Task LoadAreasAsync()
        {
            Areas.Clear();

            List<AreaDto> areas = await _getAllAreasHandler.Handle();
            foreach (AreaDto area in areas)
            {
                Areas.Add(area);
            }
        }
    }
}
