using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TeamTaskManagementAPI.Business.Helper;
using TeamTaskManagementAPI.Business.Mapper;
using TeamTaskManagementAPI.Business.Services.Implementations;
using TeamTaskManagementAPI.Business.Services.Implementations.Auth;
using TeamTaskManagementAPI.Business.Services.Implementations.Tasks;
using TeamTaskManagementAPI.Business.Services.Implementations.Teams;
using TeamTaskManagementAPI.Business.Services.Interfaces;
using TeamTaskManagementAPI.Business.Services.Interfaces.Auth;
using TeamTaskManagementAPI.Business.Services.Interfaces.Teams;

namespace TeamTaskManagementAPI.Business;

public static class DependencyInjection
{
    /// <summary>
    /// Dependency injection for infra layer.
    /// </summary>
    /// <param name="services">Service collection.</param>
    /// <param name="configuration">Configuration.</param>
    /// <returns>return collection.</returns>
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(ProfileMapping).Assembly);
        services.AddScoped<ITaskService, TaskService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITeamService, TeamService>();
        services.AddScoped<IUserService, UserService>();
        
        return services;
    }
}