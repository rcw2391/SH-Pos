namespace DataLayer.Models
{
    public class GiftCertificate : ICrudObject
    {
        public int ID { get; set; }
        public decimal Amount { get; set; }
        public int StylistId { get; set; }
        public Stylist IssuedBy { get; set; }
    }
}
