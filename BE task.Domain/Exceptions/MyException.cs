namespace BE_task.Domain.Exceptions
{
    public class MyException : Exception
    {
        public ErrorCode ErrorCode { get; private set; }

        public MyException(string message, ErrorCode errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }

    public enum ErrorCode
    {
        Unknown = 0,
        Validation = 400,
        NotFound = 404,
        Conflict = 409,
        Internal = 500
    }
}
