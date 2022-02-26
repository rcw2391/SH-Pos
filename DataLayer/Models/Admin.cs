using Common;

namespace DataLayer.Models
{
    public class Admin : PropertyChangedBase, ICrudObject
    {
        private int _id;
        public int ID 
        {
            get => _id;
            set => SetProperty(ref _id, value);
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
    }
}
