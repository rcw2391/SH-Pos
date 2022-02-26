namespace DataLayer.Models
{
    public class Admin : ICrudObject
    {
        public int ID { get; set; }
        
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
