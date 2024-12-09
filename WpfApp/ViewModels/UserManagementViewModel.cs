using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Application.UseCase.CreateUser;
using Application.UseCase.GetLast10UsersHandler;
using Application.UseCase.UpdateContactData;
using GalaSoft.MvvmLight.Command;

namespace WpfApp.ViewModels
{
    public class UserManagementViewModel : ViewModelBase
    {
        private readonly CreateUserHandler _createUserHandler;
        private readonly GetLast10UsersHandler _getLast10UsersHandler;
        private readonly UpdateContactDataHandler _updateContactDataHandler;
        private ObservableCollection<UserDto> _users;

        public ObservableCollection<UserDto> Users
        {
            get => _users;
            set => SetProperty(ref _users, value);
        }

        public ICommand LoadUsersCommand { get; }
        public ICommand CreateUserCommand { get; }
        public ICommand UpdateContactDataCommand { get; }
        

        public UserManagementViewModel(
            CreateUserHandler createUserHandler,
            GetLast10UsersHandler getLast10UsersHandler,
            UpdateContactDataHandler updateContactDataHandler
        )
        {
            _createUserHandler = createUserHandler;
            _getLast10UsersHandler = getLast10UsersHandler;
            _updateContactDataHandler = updateContactDataHandler;
            Users = new ObservableCollection<UserDto>();
            LoadUsersCommand = new RelayCommand(async () => await LoadUsersAsync());
            CreateUserCommand = new RelayCommand<CreateUserCommand>(async parameter => await CreateUserAsync(parameter));
            UpdateContactDataCommand = new RelayCommand<UpdateContactDataCommand>(async parameter => await UpdateUserAsync(parameter));
            Task.Run(() => LoadUsersAsync());
        }

        private async Task LoadUsersAsync()
        {
            if (Users == null)
            {
                Users = new ObservableCollection<UserDto>();
            }

            Users.Clear();

            List<UserDto> users = await _getLast10UsersHandler.Handle();
            int counter = 1;
            foreach (UserDto user in users)
            {
                Users.Add(user);
                user.Index = counter++;
            }
        }

        public async Task CreateUserAsync(CreateUserCommand newUser)
        {
            await _createUserHandler.Handle(newUser);
            await LoadUsersAsync();
        }

        public async Task UpdateUserAsync(UpdateContactDataCommand contactData)
        {
            await _updateContactDataHandler.Handle(contactData);
            await LoadUsersAsync();
        }
    }
}
