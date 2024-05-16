using MediatR;
using RecruitmentProcessDomain.BindingModels;
using RecruitmentProcessDomain.Models;
using RecruitmentProcessInfrastructure.Persistence.Context;
using RecruitmentProcessInfrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentProcessBusiness.QuestionTypes.Commands.Create
{
    public class CreateQuestionTypeCommandHandler : IRequestHandler<CreateQuestionTypeCommand, CreateQuestionTypeCommandResponse>
    {
        private readonly IAsyncRepository<QuestionType> _questionTypeRepository;
        public CreateQuestionTypeCommandHandler(IAsyncRepository<QuestionType> questionTypeRepository)
        {
            _questionTypeRepository = questionTypeRepository;
        }

        public  async Task<CreateQuestionTypeCommandResponse> Handle(CreateQuestionTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new CreateQuestionTypeCommandResponse();
            try
            {
                var validate = new CreateQuestionTypeCommandValidator();
                var validateResult = await validate.ValidateAsync(request);
                if (validateResult.Errors.Any()) throw new Exception(string.Join(Environment.NewLine,validateResult));
                var itemExist = await _questionTypeRepository.GetSingleAsync(_=>_.Type == request.Type);
                if (itemExist !=null) throw new Exception(request.Type + " already exist.");
                var program = new QuestionType
                {
                    Type = request.Type,
                };
                await _questionTypeRepository.AddAsync(program);
                response.Success = true;
                response.Message = Constant.SuccessCreated;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
