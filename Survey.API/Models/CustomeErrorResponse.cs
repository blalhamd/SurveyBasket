namespace Survey.API.Models
{
    public class CustomeErrorResponse
    {
        public CustomeErrorResponse()
        {
            
        }
        public CustomeErrorResponse(int statusCode, string? message, string? details)
        {
            StatusCode = statusCode;
            Message = message ?? TakeDefault(statusCode);
            Details = details;
        }

        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public string? Details { get; set; }

        public override string ToString()
        {
            return $"StatusCode: {StatusCode}\n" +
                   $"Message: {Message}\n" +
                   $"Details: {Details}\n";
        }

        private string? TakeDefault(int statusCode)
        {
            switch(statusCode)
            {
                case 400:
                    return "Bad Request";
                case 401:
                    return "you are not authorzied";
                case 405:
                    return "Method Not allowed";
                case 409:
                    return "Conflict";
                default:
                    return "Resourse is not found";
            }
        }

    }

    public class ValidationErrorResponse : CustomeErrorResponse
    {
        public IList<string> Errors { get; set; }

        public ValidationErrorResponse():base(400,null,null)
        {
            Errors = new List<string>();
        }

        public ValidationErrorResponse(int statusCode, string? message, string? details, IList<string> errors)
            :base(statusCode, message, details)
        {
            Errors = errors;
        }
    }
}
