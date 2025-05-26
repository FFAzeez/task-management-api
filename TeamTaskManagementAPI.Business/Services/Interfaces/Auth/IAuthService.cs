using TeamTaskManagementAPI.Domain.BindingModels.Requests;
using TeamTaskManagementAPI.Domain.BindingModels.Response;

namespace TeamTaskManagementAPI.Business.Services.Interfaces.Auth;

public interface IAuthService
{
    Task<BaseResponse<UserResponse>> RegisterAsync(UserRegisterRequest request);
    Task<BaseResponse<LoginResponse>> LoginAsync(UserLoginRequest request);
    Task<BaseResponse<UserResponse>> GetCurrentUserInfoAsync(string userId);
}