namespace Repo_API_GeneralCatalog_1721030646.Helpers
{
    public class ResponseAPI<T>
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; } = string.Empty;
        public T Data { get; set; }

        public ResponseAPI(bool success, string message, T data)
        {
            Success = success;
            Message = message;
            Data = data;
        }
    }

    public class ResponseData
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; } = string.Empty;
        public dynamic Data { get; set; } = null!;

        //public ResponseAPI()
        //{            
        //    Data = null;
        //}
        //public ResponseAPI(bool success, string message, dynamic data)
        //{
        //    Success = success;
        //    Message = message;
        //    Data = data;
        //}
    }
}
