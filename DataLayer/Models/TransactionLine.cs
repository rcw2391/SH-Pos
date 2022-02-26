using Dapper.Contrib.Extensions;

namespace DataLayer.Models
{
    public class TransactionLine : ICrudObject
    {
        public int ID { get; set; }
        public int TransactionId { get; set; }
        public decimal Amount { get; set; }
        public decimal Tax { get; set; }
        public int StylistId { get; set; }
        public int? ProductId { get; set; }
        public int? ServiceId { get; set; }
        [Computed]
        public Transaction Transaction { get; set; }
        [Computed]
        public Product Product { get; set; }
        [Computed]
        public Service Service { get; set; }
        [Computed]
        public Stylist Stylist { get; set; }

    }
}
