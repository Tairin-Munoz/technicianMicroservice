    
namespace technicianMicroservice.Domain.Services
{
    public class Result
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;

        public List<string> Errors { get; }
        public string Error => Errors.FirstOrDefault() ?? string.Empty;

        protected Result(bool isSuccess, List<string>? errors)
        {
            IsSuccess = isSuccess;
            Errors = errors ?? new List<string>();
        }

        public static Result Success() => new Result(true, new List<string>());

        public static Result Failure(string error) =>
            new Result(false, new List<string> { error });

        public static Result Failure(List<string> errors) =>
            new Result(false, errors);
    }

    public class Result<T> : Result
    {
        public T? Value { get; }

        private Result(bool isSuccess, T? value, List<string>? errors)
            : base(isSuccess, errors)
        {
            Value = value;
        }

        public static Result<T> Success(T value) =>
            new Result<T>(true, value, new List<string>());

        public new static Result<T> Failure(string error) =>
            new Result<T>(false, default, new List<string> { error });

        public new static Result<T> Failure(List<string> errors) =>
            new Result<T>(false, default, errors);
    }
}
