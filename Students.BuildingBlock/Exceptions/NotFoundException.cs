﻿namespace Students.BuildingBlock.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException() : base("Not Found!")
    {
    }
    public NotFoundException(string message) : base(message)
    {
    }
}