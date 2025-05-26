
namespace TeamTaskManagementAPI.Domain.BindingModels.Response
{
    public class BaseResponse
    {
        public BaseResponse()
        {
            Success = false;
        }

        public BaseResponse(string message)
        {
            Success = false;
            Message = message;
        }

        public BaseResponse(string message, bool success)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }
    
    public class BaseResponse<T>: BaseResponse
    {
        public T Data { get; set; }
    }
}
