using System.Net;

namespace ChallengeCompraGamer_Backend.Models
{
    public class Result<T>
    {
        public bool IsSuccess { get; init; }
        public string Message { get; init; }
        public HttpStatusCode StatusCode { get; init; }
        public T? Data { get; init; }

        public static Result<T> Success(T data, HttpStatusCode statusCode = HttpStatusCode.OK, string? message = null) =>
            new() { IsSuccess = true, Data = data, StatusCode = statusCode, Message = message };

        public static Result<T> Failure(string message, HttpStatusCode statusCode) =>
            new() { IsSuccess = false, Message = message, StatusCode = statusCode };
    }
}
