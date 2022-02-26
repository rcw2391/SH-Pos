namespace DataLayer.Models
{
    public class Stylist : ICrudObject
    {
        public int ID { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public bool IsAdmin { get; set; }
        public bool IsManager { get; set; }

        public bool IsActive { get; set; }
    }
}
