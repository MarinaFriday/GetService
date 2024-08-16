namespace GetServise.Models
{
    public class Calendar
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }     
        public string DayOfWeek { get; set; }
        public TypeOfDay TypeOfDay { get; set; }
    }
}
