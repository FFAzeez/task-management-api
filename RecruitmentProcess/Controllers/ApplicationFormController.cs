using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecruitmentProcessBusiness.Applications.Commands.Create;

namespace RecruitmentProcess.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationFormController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ApplicationFormController> _logger;
        public ApplicationFormController(IMediator mediator, ILogger<ApplicationFormController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        [HttpPost("", Name = "Create Application Form")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CreateApplicationFormCommandResponse>> CreateApplicationFormCommand([FromBody] CreateApplicationFormCommand model)
        {
            return Ok(await _mediator.Send(model));
        }
    }
}
