using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentProcessDomain.BindingModels
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
}
