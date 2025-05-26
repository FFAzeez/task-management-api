using TeamTaskManagementAPI.Domain.BindingModels.Requests;
using TeamTaskManagementAPI.Domain.BindingModels.Response;
using TeamTaskManagementAPI.Domain.Enums;

namespace TeamTaskManagementAPI.Business.Services.Interfaces;

public interface ITaskService
{
    Task<BaseResponse<IEnumerable<TaskResponse>>> GetTasksByTeamIdAsync(string teamId);
    Task<BaseResponse> CreateTaskAsync(string TeamId, CreateTaskRequest request);
    Task<BaseResponse> UpdateTaskAsync(string TaskId,UpdateTaskRequest request);
    Task<BaseResponse> DeleteTaskAsync(string TaskId);
    Task<BaseResponse> UpdateTaskStatusAsync(string TaskId,TasksStatus status);
}