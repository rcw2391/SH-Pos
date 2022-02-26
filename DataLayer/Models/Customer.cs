using Dapper.Contrib.Extensions;

namespace DataLayer.Models
{
    public class Customer : ICrudObject
    {
        [Key]
        public int ID { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Formula { get; set; }

        public string Duration { get; set; }

        public string OldID { get; set; }

        [Computed]
        public string PhoneDisplay
        {
            get
            {
                if (Phone == null) return "";

                return Phone.Substring(0, 3) + "-" + Phone.Substring(3, 3) + "-" + Phone.Substring(6, 4);
            }
        }

        [Computed]
        public virtual List<Transaction> Transactions { get; set; }
    }
}
