using Dapper.Contrib.Extensions;

namespace DataLayer.Models
{
    public class Transaction : ICrudObject
    {
        public int ID { get; set; }

        public int CreatedById { get; set; }
        public int CustomerId { get; set; }
        public string Details { get; set; }

        public decimal GrandTotal { get; set; }

        public decimal ProductTotal { get; set; }

        public decimal ServiceTotal { get; set; }
        public decimal GiftCertificateTotal { get; set; }

        public decimal Taxes { get; set; }

        public bool IsVoid { get; set; }

        public string LegacyID { get; set; }

        public DateTime Date { get; set; }
        [Computed]
        public List<TransactionLine> TransactionLines { get; set; }
        [Computed]
        public List<Payment> Payments { get; set; }
        [Computed]
        public List<GiftCertificate> GiftCertificates { get; set; }
        [Computed]
        public Stylist Stylist { get; set; }
        [Computed]
        public Customer Customer { get; set; }
    }
}
