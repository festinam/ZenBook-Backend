namespace ZenBook_Backend.Shared
{
    public class Result<T>
    {
        public T? Data { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string? SuccessMessage { get; set; }
        public int StatusCode { get; set; }
        public ErrorModel? Error { get; set; }

        public static Result<T> NotFound(ErrorModel model)
        {
            return new Result<T> { IsSuccess = false, Error = model , StatusCode = StatusCodes.Status404NotFound };
        }

        public static Result<T> BadRequest(ErrorModel model)
        {
            return new Result<T> { IsSuccess = false, Error = model, StatusCode = StatusCodes.Status400BadRequest };
        }
        public static Result<T> Ok(T? data,string? successMessage = null)
        {
            return new Result<T> { Data = data,IsSuccess = true, StatusCode = StatusCodes.Status200OK, SuccessMessage = successMessage };

        }


    }
    public record ErrorModel(string Code, string Message);
   
    }
