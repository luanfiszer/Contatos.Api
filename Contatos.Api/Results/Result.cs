namespace Contatos.Api.Results
{
    public class Result
    {
        public bool Success { get; }
        public string Message { get; }

        protected Result(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public static Result Ok(string message = null)
            => new Result(true, message);

        public static Result Fail(string message)
            => new Result(false, message);
    }

    public class Result<T> : Result
    {
        public T Data { get; }

        private Result(bool success, T data, string message)
            : base(success, message)
        {
            Data = data;
        }

        public static Result<T> Ok(T data, string message = null)
            => new Result<T>(true, data, message);

        public static new Result<T> Fail(string message)
            => new Result<T>(false, default!, message);
    }
}
