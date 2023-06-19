namespace SafeEntry.Core.Responses
{
    public class ResponseBase
    {
        public bool Success { get; set; }   
        public IList<string> ValidationErrors { get; set; } 
        public ResponseBase()
        {
            Success = true;
            ValidationErrors = new List<string>();
        }
    }
}
