namespace Students.BuildingBlock.Evetns
{
    public class StudentCreateEvent: BaseEvent
    {
        public int StudentId { get; set; }
        public DateTime CreatedOnUtc { get; set; }
    }
}
