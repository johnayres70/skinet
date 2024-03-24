
namespace API.Errors
{
    // because we dont have a parameterless constructor in our ApiResponce 
    // we have to provide a constructor inside this class that is deriving from it.

    public class ApiException : ApiResponse
    {
        public ApiException(int statusCode, string message = null, string details = null)
            : base(statusCode, message)
        {
            Details = details;
        }

        public string Details { get; set; } // will contain stack trace details
    }
}