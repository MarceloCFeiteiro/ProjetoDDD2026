namespace WebApi.Models
{
    public class ErrorResponse
    {
        public bool Success => false;

        public string Message { get; set; }

        public IEnumerable<string> Errors {  get; set; }
    }
}
