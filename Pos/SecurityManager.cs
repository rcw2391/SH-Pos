using Common;
using DataLayer;
using DataLayer.Models;

namespace Pos
{
    public sealed class SecurityManager : PropertyChangedBase
    {
        private static readonly SecurityManager _instance = new();

        static SecurityManager() { }
        private SecurityManager() { }

        public static SecurityManager Instance => _instance;

        private IUnitOfWork _uow;

        private bool IsAdmin => User is not null && User.IsAdmin;
        public bool IsLoggedIn => User is not null;

        private User _user;
        public User User
        {
            get => _user;
            set
            {
                SetProperty(ref _user, value);
                OnPropertyChanged(nameof(IsAdmin));
                OnPropertyChanged(nameof(IsLoggedIn));
            }
        }

        public void SetUnitOfWork(IUnitOfWork uow)
        {
            _uow = uow;
        }

        private void Login(User u)
        {
            User = u;
        }

        public void TryLogin(string username, string password)
        {

        }

        public void LoginRhino()
        {
            Tasker<User> _ = new(_uow.Users.GetByIdAsync(1), u =>
            {
                User = u;
            });
        }
    }
}
