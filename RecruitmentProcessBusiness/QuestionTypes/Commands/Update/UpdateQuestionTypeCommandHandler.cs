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

namespace RecruitmentProcessBusiness.QuestionTypes.Commands.Update
{
    public class UpdateQuestionTypeCommandHandler : IRequestHandler<UpdateQuestionTypeCommand, UpdateQuestionTypeCommandResponse>
    {
        private readonly IAsyncRepository<QuestionType> _questionTypeRepository;
        public UpdateQuestionTypeCommandHandler(IAsyncRepository<QuestionType> questionTypeRepository)
        {
            _questionTypeRepository = questionTypeRepository;
        }
        public async Task<UpdateQuestionTypeCommandResponse> Handle(UpdateQuestionTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new UpdateQuestionTypeCommandResponse();
            try
            {
                var validate = new UpdateQuestionTypeCommandValidator();
                var validateResult = await validate.ValidateAsync(request);
                if (validateResult.Errors.Any()) throw new Exception(string.Join(Environment.NewLine, validateResult));
                var itemExist = await _questionTypeRepository.GetSingleAsync(_ => _.Id == request.Id);
                if (itemExist == null) throw new Exception(Constant.NotFund);
                itemExist.Type = request.Type;
                await _questionTypeRepository.UpdateAsync(itemExist);
                response.Success = true;
                response.Message = Constant.Update;
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
