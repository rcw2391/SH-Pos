namespace DataConversion.OldShape
{
    public class OldTransaction
    {
        public string details { get; set; }
        public TransNumber productTotal { get; set; }
        public TransNumber serviceTotal { get; set; }
        public TransNumber taxes { get; set; }
        public string stylist { get; set; }
        public OldId customerId { get; set; }
        public TransNumber grandTotal { get; set; }
        public TransNumber paymentTotal { get; set; }
        public List<OldServiceTransaction> services { get; set; }
        public List<OldProductTransaction> products { get; set; }
        public TransNumber dateDate { get; set; }
        public string dateMonth { get; set; }
        public TransNumber dateYear { get; set; }
        public TransNumber change { get; set; }
        public List<OldPayment> payments { get; set; }
        public List<OldGiftCertificate> giftCerts { get; set; }
        public OldId _id { get; set; }
    }
}
