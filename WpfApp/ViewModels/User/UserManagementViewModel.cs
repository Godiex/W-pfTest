using Application.CreateUser;
using GalaSoft.MvvmLight.Command;
using MediatR;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp.ViewModels.User;

namespace WpfApp.ViewModels
{
    public class UserManagementViewModel : ViewModelBase
    {
        private ObservableCollection<UserDto> _users;
        public ObservableCollection<UserDto> Users
        {
            get => _users;
            set => SetProperty(ref _users, value);
        }

        private readonly IMediator _mediator;

        //public ICommand LoadUsersCommand { get; }
        public ICommand CreateUserCommand { get; }

        public UserManagementViewModel(IMediator mediator)
        {
            _mediator = mediator;

            //LoadUsersCommand = new RelayCommand(async () => await LoadUsersAsync());
            CreateUserCommand = new RelayCommand(async () => await CreateUserAsync());
        }

        //private async Task LoadUsersAsync()
        //{
        //    var users = await _mediator.Send(new GetUsersQuery());
        //    Users = new ObservableCollection<UserDto>(users);
        //}

        private async Task CreateUserAsync()
        {
            UserDto newUser = new UserDto
            {
                Identification = "123456789",
                FullName = "Juan Pérez",
                Email = "juan.perez@example.com",
                Phone = "1234567890"
            };  

            await _mediator.Send(new CreateUserCommand(newUser.Identification, newUser.FullName, newUser.Email, newUser.Phone));
            //await LoadUsersAsync();
        }
    }
}
