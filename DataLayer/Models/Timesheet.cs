namespace DataLayer.Models
{
    public class Timesheet : ICrudObject
    {
        public int ID { get; set; }

        public Stylist Stylist { get; set; }

        public DateTime EndDay { get; set; }

        public List<TimesheetLine> Lines { get; set; }
    }
}
