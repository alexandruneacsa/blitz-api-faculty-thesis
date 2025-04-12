namespace Blitz.Application.Dtos
{
    public class BlitzWrapper<T>
    {
        public BlitzWrapper()
        {
            ErrorMessage = "";
            StatusCode = 0;
            ObjectResponse = default(T);
        }

        public BlitzWrapper(string errorMessage, T? objectResponse, int statusCode)
        {
            ErrorMessage = errorMessage;
            ObjectResponse = objectResponse;
            StatusCode = statusCode;
        }

        public string ErrorMessage { get; set; }
        public int StatusCode { get; set; }
        public T? ObjectResponse { get; set; }
    }
}
