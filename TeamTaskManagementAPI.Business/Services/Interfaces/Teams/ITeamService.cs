using TeamTaskManagementAPI.Domain.BindingModels.Requests;
using TeamTaskManagementAPI.Domain.BindingModels.Response;

namespace TeamTaskManagementAPI.Business.Services.Interfaces.Teams;

public interface ITeamService
{
    Task<BaseResponse> CreateTeamAsync(CreateTeamRequest request);
    Task<BaseResponse> AddUserToTeamAsync(string TeamId, AddUserToTeamRequest request);
}