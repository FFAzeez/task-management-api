using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RecruitmentProcessBusiness.Helper;
using TeamTaskManagementAPI.Business.Services.Interfaces.Auth;
using TeamTaskManagementAPI.Domain.BindingModels.Common;
using TeamTaskManagementAPI.Domain.BindingModels.Requests;
using TeamTaskManagementAPI.Domain.BindingModels.Response;
using TeamTaskManagementAPI.Domain.Models;
using TeamTaskManagementAPI.Infrastructure.Persistence.Repositories;

namespace TeamTaskManagementAPI.Business.Services.Implementations.Auth;

public class AuthService(IAsyncRepository<User> userRepository,IAsyncRepository<TeamUser> teamUserRepository, 
    IConfiguration configuration, IMapper mapper):IAuthService
{
    public async Task<BaseResponse<UserResponse>> RegisterAsync(UserRegisterRequest request)
    {
        var existingUser = await userRepository.GetSingleAsync(_ => _.Email == request.Email);
        if (existingUser != null)
            throw new InvalidOperationException("Email already exists");

        var user = mapper.Map<User>(request);
        user.PasswordHash = request.Password.GenerateHash();

        var result = await userRepository.AddAsync(user);
        var map = mapper.Map<UserResponse>(result);
        var response = new BaseResponse<UserResponse>
        {
            Message = Constant.Success,
            Success = true,
            Data = map
        };
        return response;
    }

    public async Task<BaseResponse<LoginResponse>> LoginAsync(UserLoginRequest request)
    {
        string role =null;
        var user = await userRepository.GetSingleAsync(_ =>
            _.Username == request.Username || _.Email == request.Username);
        if (user == null || !request.Password.VerifyHash(user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid credentials");
        
        var teamUser = await teamUserRepository.GetSingleAsync(_ =>_.UserId == user.Id);
        if(teamUser != null) role = teamUser.Role;
        
        var token = await CreateToken(user, role);
        var map = mapper.Map<LoginResponse>(user);
        map.Token = token;
        map.LoginDate = DateTime.Now;
        user.LastLoginDate = map.LoginDate;
        userRepository.UpdateAsync(user);
        var response = new BaseResponse<LoginResponse>
        {
            Message = Constant.Success,
            Success = true,
            Data = map
        };
        return response;
    }

    public async Task<BaseResponse<UserResponse>> GetCurrentUserInfoAsync(string userId)
    {
        var user = await userRepository.GetSingleAsync(_=>_.Id == userId);
        if (user == null)
            throw new KeyNotFoundException(Constant.UserNotFound);
        var map = mapper.Map<UserResponse>(user);
        var response = new BaseResponse<UserResponse>
        {
            Success = true,
            Message = Constant.Success,
            Data = map
        };
        return response;
    }

    /// <summary>
    /// generate token method
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    private async Task<string> CreateToken(User user,string? Role)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, Role),
                new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(new TokenUserData()
                {
                    Email = user.Email,
                    UserId = user.Id,
                     Role = Role,
                     Username = user.Username,
                }))
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = configuration["Jwt:Issuer"],
            Audience = configuration["Jwt:Audience"],
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}