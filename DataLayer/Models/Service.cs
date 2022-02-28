using Common;

namespace DataLayer.Models
{
    public class Service : PropertyChangedBase, ICrudObject, ICacheable
    {
        private int _iD;
        public int ID
        {
            get => _iD;
            set => SetProperty(ref _iD, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private decimal _price;
        public decimal Price
        {
            get => _price;
            set => SetProperty(ref _price, value);
        }

        private bool _isCustom;
        public bool IsCustom
        {
            get => _isCustom;
            set => SetProperty(ref _isCustom, value);
        }

        private string _oldID;
        public string OldID
        {
            get => _oldID;
            set => SetProperty(ref _oldID, value);
        }
        private bool _isDeleted;
        public bool IsDeleted
        {
            get => _isDeleted;
            set => SetProperty(ref _isDeleted, value);
        }
    }
}
