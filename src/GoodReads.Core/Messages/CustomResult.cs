namespace GoodReads.Core.Messages
{
    public class CustomResult
    {
        private CustomResult(bool isSuccess, string message, string[] errors = null, object data = null) 
        {
            IsSuccess = isSuccess;
            Message = message;
            Errors = errors;
            Data = data;
        }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string[] Errors { get; set; }
        public object Data { get; set; }

        public static CustomResult Success(string message, object data = null)
        {
            return new CustomResult(true, message, null, data);
        }

        public static CustomResult Failure(string message, string[] errors = null)
        {
            return new CustomResult(false, message, errors);
        }
    }
}
