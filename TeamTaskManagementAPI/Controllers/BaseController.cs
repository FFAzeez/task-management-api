using Microsoft.AspNetCore.Mvc;
using TeamTaskManagementAPI.Domain.BindingModels.Response;

namespace TeamTaskManagementAPI.Controllers
{
    public class BaseController : ControllerBase
    {
        private readonly string _env;
        public BaseController()
        { }

        public BaseController(IWebHostEnvironment env)
        {
            _env = env.EnvironmentName;
        }

        internal IActionResult HandleException(Exception ex, ILogger logger, string env)
        {
            logger.LogError(ex, ex.Message);

            if (env != null && (env == "local" || env.ToLower() == "development" || env == "uat"))
            {
                return StatusCode(500, new BaseResponse()
                {
                    Success = false,
                    //Trace = $"{ex.Message}, {ex.StackTrace}",
                    Message = ex.Message
                });
            }
            else
            {
                return StatusCode(500, new BaseResponse()
                {
                    Success = false,
                    //Trace = $"{ex.Message}, {ex.StackTrace}",
                    Message = "Something went wrong. It’s not you, it’s us. Please give it another try."
                });
            }
        }
    }
}
