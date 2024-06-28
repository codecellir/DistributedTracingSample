namespace Students.BuildingBlock
{
    public class Result<T>
    {
        public bool Succeeded { get; set; }
        public T? Data { get; set; }
        public string? ErrorMessage { get; set; }

        public static Result<T> Success(T data)
        {
            return new Result<T> { Data = data, Succeeded = true };
        }
        public static Result<T> Fail(string errorMessage = "")
        {
            return new Result<T> { ErrorMessage = errorMessage };
        }
    }

    public class BaseResult : Result<int?>
    {
        public static BaseResult Success()
        {
            return new BaseResult { Succeeded = true };
        }
        public new static BaseResult Fail(string errorMessage = "")
        {
            return new BaseResult { ErrorMessage = errorMessage };
        }
    }
}