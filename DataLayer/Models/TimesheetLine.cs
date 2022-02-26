namespace DataLayer.Models
{
    public class TimesheetLine : ICrudObject
    {
        public int ID { get; set; }

        public Timesheet TimeSheet { get; set; }

        public double Hours { get; set; }

        public DateTime Date { get; set; }
    }
}
