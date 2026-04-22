namespace YouScout.Application.Common.Models;

public sealed class Result
{
    public bool Succeeded { get; init; }

    public string[] Errors { get; init; }
    
    private Result(bool succeeded, IEnumerable<string> errors)
    {
        Succeeded = succeeded;
        Errors = errors.ToArray();
    }
    
    public static Result Success()
    {
        return new Result(true, []);
    }

    public static Result Failure(IEnumerable<string> errors)
    {
        return new Result(false, errors);
    }
}