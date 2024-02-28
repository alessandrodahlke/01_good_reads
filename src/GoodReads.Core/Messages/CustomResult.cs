namespace GoodReads.Core.Messages
{
    public class CustomResult
    {
        public CustomResult(bool success, string message, string[] errors = null, object data = null) 
        {
            Success = success;
            Message = message;
            Errors = errors;
            Data = data;
        }
        public bool Success { get; set; }
        public string Message { get; set; }
        public string[] Errors { get; set; }
        public object Data { get; set; }
    }
}
