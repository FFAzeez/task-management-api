using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RecruitmentProcessBusiness.ProgramDetails.Commands.Create;
using RecruitmentProcessBusiness.ProgramDetails.Commands.Delete;
using RecruitmentProcessBusiness.ProgramDetails.Commands.Update;
using RecruitmentProcessBusiness.QuestionTypes.Commands.Create;
using RecruitmentProcessBusiness.QuestionTypes.Commands.Update;
using RecruitmentProcessBusiness.QuestionTypes.Queries.GetAll;
using RecruitmentProcessBusiness.QuestionTypes.Queries.GetIndividual;

namespace RecruitmentProcess.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionTypeController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<QuestionTypeController> _logger;
        public QuestionTypeController(IMediator mediator, ILogger<QuestionTypeController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        [HttpPost("", Name ="Create Question Type for recruitment ")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CreateQuestionTypeCommandResponse>> CreateProgramDetailsCommand([FromBody] CreateQuestionTypeCommand model)
        {
            return Ok(await _mediator.Send(model));
        }

        [HttpPost("update", Name = "Update Question Type for recruitment ")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UpdateQuestionTypeCommandResponse>> UpdateProgramDetailsCommand([FromBody] UpdateQuestionTypeCommand model)
        {
            return Ok(await _mediator.Send(model));
        }

        [HttpGet("getall", Name = "Get All Program details")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GetAllQuestionTypeQueryResponse>> GetAllProgramDetailQuery([FromQuery] GetAllQuestionTypeQuery model)
        {
            return Ok(await _mediator.Send(model));
        }
        [HttpGet("get", Name = "Get individual Program details")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GetAllQuestionTypeQueryResponse>> GetProgramDetailQuery([FromQuery] GetQuestionTypeQuery model)
        {
            return Ok(await _mediator.Send(model));
        }
    }
}
