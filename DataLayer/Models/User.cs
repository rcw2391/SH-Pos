using Common;
using Dapper.Contrib.Extensions;

namespace DataLayer.Models
{
    public class User : PropertyChangedBase, ICrudObject, ICacheable
    {
        private int _iD;
        public int ID
        {
            get => _iD;
            set => SetProperty(ref _iD, value);
        }
        private int? _stylistId;
        public int? StylistId
        {
            get => _stylistId;
            set => SetProperty(ref _stylistId, value);
        }
        private string _username;
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }
        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }
        private bool _isAdmin;
        public bool IsAdmin
        {
            get => _isAdmin;
            set => SetProperty(ref _isAdmin, value);
        }

        private bool _isActive;
        public bool IsActive
        {
            get => _isActive;
            set => SetProperty(ref _isActive, value);
        }

        private bool _isDeleted;
        [Computed]
        public bool IsDeleted
        {
            get => _isDeleted;
            set => SetProperty(ref _isDeleted, value);
        }
    }
}
