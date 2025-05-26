using AutoMapper;
using Microsoft.Extensions.Configuration;
using TeamTaskManagementAPI.Business.Services.Interfaces.Teams;
using TeamTaskManagementAPI.Domain.BindingModels.Common;
using TeamTaskManagementAPI.Domain.BindingModels.Requests;
using TeamTaskManagementAPI.Domain.BindingModels.Response;
using TeamTaskManagementAPI.Domain.Models;
using TeamTaskManagementAPI.Infrastructure.Persistence.Repositories;

namespace TeamTaskManagementAPI.Business.Services.Implementations.Teams;

public class TeamService(IAsyncRepository<Team> teamRepository, IAsyncRepository<TeamUser> teamUserRepository,
    IAsyncRepository<User> userRepository):ITeamService
{
    /// <summary>
    ///  Create Team that can exist.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<BaseResponse> CreateTeamAsync(CreateTeamRequest request)
    {
        var team = new Team
        {
            Name = request.Name,
        };
        await teamRepository.AddAsync(team);

        return new BaseResponse()
        {
            Success = Constant.SuccessBool,
            Message = Constant.SuccessCreated
        };
    }
    
   // dont forget the permission for this method
    /// <summary>
    /// Adding user to team is done by admin 
    /// </summary>
    /// <param name="request"></param>
    /// <exception cref="KeyNotFoundException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task<BaseResponse> AddUserToTeamAsync(string TeamId, AddUserToTeamRequest request)
    {
        var team = await teamRepository.GetSingleAsync(_=>_.Id == TeamId);
        if (team == null)
            throw new KeyNotFoundException(Constant.TaskNotFound);
        
        var user = await userRepository.GetSingleAsync(_=>_.Id == request.UserId);
        if (user == null)
            throw new KeyNotFoundException(Constant.UserNotFound);

        var existingTeamUser = await teamUserRepository.GetSingleAsync(_=>_.UserId == request.UserId 
                                                                          && _.TeamId == TeamId);
        if (existingTeamUser !=null)
            throw new InvalidOperationException(Constant.AlreadyMember);

        var teamUser = new TeamUser
        {
            UserId = request.UserId,
            TeamId = TeamId,
            Role = request.Role ?? "Member"
        };

        await teamUserRepository.AddAsync(teamUser);
        return new BaseResponse()
        {
            Success = Constant.SuccessBool,
            Message = Constant.SuccessCreated
        };
    }
}