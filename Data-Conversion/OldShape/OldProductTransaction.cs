namespace DataConversion.OldShape
{
    public class OldProductTransaction
    {
        public bool isCustom { get; set; }
        public Qty qty { get; set; }
        public string desc { get; set; }
        public TransNumber price { get; set; }
        public OldId product { get; set; }
        public OldStylistTransaction stylist { get; set; }
    }
}
