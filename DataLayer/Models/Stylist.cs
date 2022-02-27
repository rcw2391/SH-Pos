using Common;
using Dapper.Contrib.Extensions;

namespace DataLayer.Models
{
    public class Stylist : PropertyChangedBase, ICrudObject, ICacheable
    {
        private int _iD;
        public int ID
        {
            get => _iD;
            set => SetProperty(ref _iD, value);
        }
        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }
        [Computed]
        public string FullName => FirstName + " " + LastName;
        public bool IsActive { get; set; }
        [Computed]
        public bool IsDeleted { get; set; }
    }
}
