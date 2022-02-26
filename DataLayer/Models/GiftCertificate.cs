using Dapper.Contrib.Extensions;

namespace DataLayer.Models
{
    public class GiftCertificate : ICrudObject
    {
        public int ID { get; set; }
        public decimal Amount { get; set; }
        public int StylistId { get; set; }
        public int TransactionId { get; set; }
        [Computed]
        public Stylist IssuedBy { get; set; }
    }
}
