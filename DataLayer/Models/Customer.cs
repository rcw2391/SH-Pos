using Common;
using Dapper.Contrib.Extensions;
using System.Collections.ObjectModel;

namespace DataLayer.Models
{
    public class Customer : PropertyChangedBase, ICrudObject, ICacheable
    {
        private int _iD;
        [Key]
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

        private string _phone;
        public string Phone
        {
            get => _phone;
            set
            {
                SetProperty(ref _phone, value);
                OnPropertyChanged(nameof(PhoneDisplay));
            }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string _formula;
        public string Formula
        {
            get => _formula;
            set => SetProperty(ref _formula, value);
        }

        private string _duration;
        public string Duration
        {
            get => _duration;
            set => SetProperty(ref _duration, value);
        }

        private string _oldID;
        public string OldID
        {
            get => _oldID;
            set => SetProperty(ref _oldID, value);
        }

        private bool _isDeleted;
        [Computed]
        public bool IsDeleted
        {
            get => _isDeleted;
            set => SetProperty(ref _isDeleted, value);
        }

        [Computed]
        public string PhoneDisplay
        {
            get
            {
                if (Phone == null) return "";

                return Phone.Substring(0, 3) + "-" + Phone.Substring(3, 3) + "-" + Phone.Substring(6, 4);
            }
        }

        private ObservableCollection<Transaction> _transactions = new();
        [Computed]
        public virtual ObservableCollection<Transaction> Transactions
        {
            get => _transactions;
            set => SetProperty(ref _transactions, value);
        }
    }
}
