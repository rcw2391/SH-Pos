namespace DataLayer.Models
{
    public class Service : ICrudObject
    {
        public int ID { get; set; }
        
        public string Description { get; set; }

        public decimal Price { get; set; }

        public bool IsCustom { get; set; }

        public string OldID { get; set; }
    }
}
