namespace Students.BuildingBlock.Exceptions;

public class AppException : Exception
{
    public AppException() : base("Bad Request")
    {
    }
    public AppException(string message) : base(message)
    {
    }
}