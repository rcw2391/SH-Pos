using Dapper.Contrib.Extensions;

namespace DataLayer.Models
{
    public class Payment : ICrudObject
    {
        public int ID { get; set; }
        public int TransactionId { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        [Computed]
        public Transaction Transaction { get; set; }
    }
}
