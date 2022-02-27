namespace DataLayer.Models
{
    public interface ICacheable 
    {
        bool IsDeleted { get; set; }
    }
}
