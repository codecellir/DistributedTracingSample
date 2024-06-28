namespace Students.BuildingBlock.Evetns
{
    public class BaseEvent
    {
        public Guid Id { get; private set; }
        public DateTime CreationDate { get; private set; }
        public BaseEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }
        public BaseEvent(Guid id, DateTime creationDate)
        {
            Id = id;
            CreationDate = creationDate;
        }
    }
}
