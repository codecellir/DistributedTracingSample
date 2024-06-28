namespace Students.Reporting.Application.Models
{
    public class StudentReportModel
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string Type { get; set; }
        public DateTime DateOnUtc { get; set; }
    }
}
