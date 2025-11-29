using Contatos.Shared.Results;
using System.Globalization;

namespace Contatos.Api.Results
{
    public class Result
    {
        public bool Success { get; }
        public StatusCodeEnum StatusCode { get; set; }
        public string Message { get; }

        protected Result(bool success, StatusCodeEnum status, string message)
        { 
            Success = success;
            StatusCode = status;
            Message = message;
        }
        public static Result Ok(string message = null)
            => new Result(true, StatusCodeEnum.Ok, message);

        public static Result BusinessError(string message)
            => new Result(false,StatusCodeEnum.BusinessError , message);

        public static Result NotFound(string message)
            => new Result(false, StatusCodeEnum.NotFound, message);
    }

    public class Result<T> : Result
    {
        public T Data { get; }

        private Result(bool success, StatusCodeEnum status, T data, string message)
            : base(success,status, message)
        {
            Data = data;
        }


        public static Result<T> Ok(T data, string message = null)
            => new Result<T>(true,StatusCodeEnum.Ok , data, message);

        public static Result<T> BusinessError(string message)
            => new Result<T>(false,StatusCodeEnum.BusinessError , default!, message);

        public static Result<T> NotFound(string message)
            => new Result<T>(false, StatusCodeEnum.NotFound, default!, message);
    }
}
