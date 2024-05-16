using AutoMapper;
using MediatR;
using Microsoft.Azure.Cosmos.Linq;
using RecruitmentProcessBusiness.QuestionTypes.Queries.GetAll;
using RecruitmentProcessDomain.BindingModels;
using RecruitmentProcessDomain.Models;
using RecruitmentProcessInfrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentProcessBusiness.QuestionTypes.Queries.GetIndividual
{
    public class GetQuestionTypeQueryHandler : IRequestHandler<GetQuestionTypeQuery, GetQuestionTypeQueryResponse>
    {
        private readonly IAsyncRepository<QuestionType> _questionTypeRepository;
        private readonly IMapper _mapper;
        public GetQuestionTypeQueryHandler(IAsyncRepository<QuestionType> questionTypeRepository, IMapper mapper)
        {
            _questionTypeRepository = questionTypeRepository;
            _mapper = mapper;
        }
        public async Task<GetQuestionTypeQueryResponse> Handle(GetQuestionTypeQuery request, CancellationToken cancellationToken)
        {
            var response = new GetQuestionTypeQueryResponse();
            try
            {
                var result = await _questionTypeRepository.GetSingleAsync(_ => _.Id == request.Id);
                if (result == null) throw new Exception(Constant.NotFund);
                response.Result = _mapper.Map<QuestionTypeResponse>(result);
                response.Success = true;
                response.Message = Constant.Success;
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
