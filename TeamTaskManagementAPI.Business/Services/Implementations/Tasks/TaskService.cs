using AutoMapper;
using TeamTaskManagementAPI.Business.Helper;
using TeamTaskManagementAPI.Business.Services.Interfaces;
using TeamTaskManagementAPI.Domain.BindingModels.Common;
using TeamTaskManagementAPI.Domain.BindingModels.Requests;
using TeamTaskManagementAPI.Domain.BindingModels.Response;
using TeamTaskManagementAPI.Domain.Enums;
using TeamTaskManagementAPI.Domain.Models;
using TeamTaskManagementAPI.Infrastructure.Persistence.Repositories;

namespace TeamTaskManagementAPI.Business.Services.Implementations.Tasks;

public class TaskService(
    IAsyncRepository<TaskModel> taskRepository,
    IAsyncRepository<TeamUser> teamUserRepository,
    IMapper mapper, IUserService userService)
    : ITaskService
{
    /// <summary>
    ///  Get Task assigned to individual users
    /// </summary>
    /// <param name="teamId"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    /// <exception cref="UnauthorizedAccessException"></exception>
    public async Task<BaseResponse<IEnumerable<TaskResponse>>> GetTasksByTeamIdAsync(string teamId)
    {
        var userId = userService.UserDetails().UserId;
        var isMember = await teamUserRepository.GetSingleAsync(_ =>_.UserId ==userId && _.TeamId == teamId);
        if (isMember == null)
            throw new UnauthorizedAccessException("User is not a member of the team");

        var tasks = await taskRepository.GetAllByFilterAsync(_ => _.TeamId == teamId);
        var map = mapper.Map<IEnumerable<TaskResponse>>(tasks);
        return new BaseResponse<IEnumerable<TaskResponse>>
        {
            Success = Constant.SuccessBool,
            Message = Constant.Success,
            Data = map
        };
    }

    public async Task<BaseResponse> CreateTaskAsync(string TeamId, CreateTaskRequest request)
    {
        var isMember =
            await teamUserRepository.GetSingleAsync(_ => _.UserId == request.UserId && _.TeamId == TeamId);
        if (isMember == null)
            throw new UnauthorizedAccessException("User is not a member of the team");

        var task = mapper.Map<TaskModel>(request);
        task.Status = TasksStatus.Pending;

        await taskRepository.AddAsync(task);
        return new BaseResponse()
        {
            Success = Constant.SuccessBool,
            Message = Constant.SuccessCreated
        };
    }

    public async Task<BaseResponse> UpdateTaskAsync(string TaskId,UpdateTaskRequest request)
    {
        var task = await taskRepository.GetSingleAsync(_ => _.Id == TaskId);
        if (task == null)
            throw new KeyNotFoundException(Constant.TaskNotFound);

        var isMember =
            await teamUserRepository.GetSingleAsync(_ => _.UserId == request.UserId && _.TeamId == request.TeamId);
        if (isMember == null)
            throw new UnauthorizedAccessException(Constant.UserNotMember);

        task.Title = request.Title ?? task.Title;
        task.Description = request.Description ?? task.Description;
        task.DueDate = request.DueDate ?? task.DueDate;
        task.Status = TasksStatus.Pending;
        task.AssignedToUserId = request.AssignedToUserId ?? task.AssignedToUserId;

        taskRepository.UpdateAsync(task);
        return new BaseResponse()
        {
            Success = Constant.SuccessBool,
            Message = Constant.SuccessUpdate
        };
    }

    public async Task<BaseResponse> DeleteTaskAsync(string TaskId)
    {
        var task = await taskRepository.GetSingleAsync(_=>_.Id == TaskId);
        if (task == null)
            throw new KeyNotFoundException(Constant.TaskNotFound);
        var userId = userService.UserDetails().UserId;
        var canDelete = await teamUserRepository.GetSingleAsync(_=>_.UserId==userId &&_.TeamId == 
            task.TeamId);
        if (canDelete ==null)
            throw new UnauthorizedAccessException(Constant.CannotDeleteTask);
        task.IsDeleted = true;
        taskRepository.DeleteAsync(task);
        return new BaseResponse()
        {
            Success = Constant.SuccessBool,
            Message = Constant.SuccessDelete
        };
    }

    public async Task<BaseResponse> UpdateTaskStatusAsync(string TaskId,TasksStatus status)
    {
        var task = await taskRepository.GetSingleAsync(_=>_.Id == TaskId);
        if (task == null)
            throw new KeyNotFoundException(Constant.TaskNotFound);
        var userId = userService.UserDetails().UserId;
        var isMember = await teamUserRepository.GetSingleAsync(_=>_.UserId == userId && _.TeamId== task.TeamId);
        if (isMember == null)
            throw new UnauthorizedAccessException(Constant.UserNotMember);
        
        task.Status = status; 
        taskRepository.UpdateAsync(task);
        return new BaseResponse()
        {
            Success = Constant.SuccessBool,
            Message = Constant.SuccessUpdate
        };
    }
}