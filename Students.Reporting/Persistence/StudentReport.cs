namespace Students.Reporting.Persistence;

public class StudentReport
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public string Type { get; set; }
    public DateTime DateOnUtc { get; set; }
}
