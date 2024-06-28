namespace Students.BuildingBlock.Evetns
{
    public class StudentViewEvent: BaseEvent
    {
        public int StudentId { get; set; }
        public DateTime ViewedOnUtc { get; set; }
    }
}
