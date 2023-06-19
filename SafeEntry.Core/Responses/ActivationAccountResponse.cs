namespace SafeEntry.Core.Responses
{
    public class ActivationAccountResponse : ResponseBase
    {
        public bool IsTokenValid { get; set; }
        public ActivationAccountResponse()
        {
            IsTokenValid = true;
        }  
    }
}
