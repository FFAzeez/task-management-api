using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using TeamTaskManagementAPI.Domain.Models;

namespace TeamTaskManagementAPI.Business.Helper;

public class UserService(IHttpContextAccessor contextAccessor) : IUserService
{
    private readonly IHttpContextAccessor _HttpContextAccessor = contextAccessor;

    public TokenUserData? UserDetails()
    {
        TokenUserData detail = null;
        if (_HttpContextAccessor.HttpContext != null)
        {
            detail = JsonConvert.DeserializeObject<TokenUserData>(_HttpContextAccessor.HttpContext.User.FindFirstValue("UserData"));
        }
        return detail;
    }
}
public interface IUserService
{
    TokenUserData UserDetails();
}
