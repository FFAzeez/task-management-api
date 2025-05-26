using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamTaskManagementAPI.Business.Services.Interfaces;
using TeamTaskManagementAPI.Domain.BindingModels.Requests;
using TeamTaskManagementAPI.Domain.BindingModels.Response;
using TeamTaskManagementAPI.Domain.Enums;

namespace TeamTaskManagementAPI.Controllers;

[Route("api")]
[ApiController]
[Authorize]
public class TasksController(ITaskService taskService) : BaseController
{

    [HttpGet("/teams/{teamId}/tasks")]
    public async Task<ActionResult<IEnumerable<TeamResponse>>> GetTasks(string teamId)
    {
        var tasks = await taskService.GetTasksByTeamIdAsync(teamId);
        if(!tasks.Success)  return BadRequest(tasks);
        return Ok(tasks);
    }

    [HttpPost("/teams/{teamId}/tasks")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<BaseResponse>> CreateTask(string teamId, [FromBody] CreateTaskRequest request)
    {
        var createdTask = await taskService.CreateTaskAsync(teamId,request);
        if(!createdTask.Success)  return BadRequest(createdTask);
        return Ok(createdTask);
    }

    [HttpPut("/tasks/{taskId}")]
    public async Task<ActionResult<BaseResponse>> UpdateTask(string taskId, [FromBody] UpdateTaskRequest request)
    {
        var updatedTask = await taskService.UpdateTaskAsync(taskId, request);
        if(!updatedTask.Success)  return BadRequest(updatedTask);
        return Ok(updatedTask);
    }

    [HttpDelete("/tasks/{taskId}")]
    public async Task<ActionResult> DeleteTask(string taskId)
    {
        var result = await taskService.DeleteTaskAsync(taskId);
        if(!result.Success)  return BadRequest(result);
        return Ok(result);
    }

    [HttpPatch("/tasks/{taskId}/status")]
    public async Task<ActionResult<BaseResponse>> UpdateTaskStatus(string taskId,
        [FromBody] TasksStatus status)
    {
        var updatedTask = await taskService.UpdateTaskStatusAsync(taskId, status);
        if(!updatedTask.Success)  return BadRequest(updatedTask);
        return Ok(updatedTask);
    }
}