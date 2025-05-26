using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamTaskManagementAPI.Business.Services.Interfaces.Teams;
using TeamTaskManagementAPI.Domain.BindingModels.Common;
using TeamTaskManagementAPI.Domain.BindingModels.Requests;
using TeamTaskManagementAPI.Domain.BindingModels.Response;

namespace TeamTaskManagementAPI.Controllers;

[Route("api/teams")]
[ApiController]
public class TeamsController(ITeamService teamService) : BaseController
{
    [HttpPost]
    [Authorize(Roles = Constant.Admin)]
    public async Task<ActionResult<BaseResponse>> CreateTeam([FromBody] CreateTeamRequest request)
    {
        var team = await teamService.CreateTeamAsync(request);
        if(!team.Success)  return BadRequest(team);
        return Ok(team);
    }

    [HttpPost("{teamId}/users")]
    [Authorize(Roles = Constant.Admin)]
    public async Task<ActionResult> AddUserToTeam(string teamId, [FromBody] AddUserToTeamRequest request)
    {
        var addTeam = await teamService.AddUserToTeamAsync(teamId, request);
        if(!addTeam.Success)  return BadRequest(addTeam);
        return Ok(addTeam);
    }
}