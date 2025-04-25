namespace SharedKernel;

public class Result
{
    public bool IsSuccess { get; }
    public string Error { get; }

    public static Result Success() => new Result(true, null);
    public static Result Failure(string error) => new Result(false, error);

    protected Result(bool isSuccess, string error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }
}

public class Result<T> : Result
{
    public T Value { get; }

    private Result(bool isSuccess, string error, T value)
        : base(isSuccess, error)
    {
        Value = value;
    }

    public static Result<T> Success(T value) => new Result<T>(true, null, value);
    public static Result<T> Failure(T value, string error) => new Result<T>(false, error, value);
}