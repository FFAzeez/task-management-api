using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.VisualBasic;
using RecruitmentProcessBusiness.Helper;
using RecruitmentProcessDomain.BindingModels;
using RecruitmentProcessDomain.Models;
using RecruitmentProcessInfrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentProcessBusiness.QuestionTypes.Queries.GetAll
{
    public class GetAllQuestionTypeQueryHandler : IRequestHandler<GetAllQuestionTypeQuery, GetAllQuestionTypeQueryResponse>
    {
        private readonly IAsyncRepository<QuestionType> _questionTypeRepository;
        private readonly IMapper _mapper;
        public GetAllQuestionTypeQueryHandler(IAsyncRepository<QuestionType> questionTypeRepository, IMapper mapper)
        {
            _questionTypeRepository = questionTypeRepository;
            _mapper = mapper;
        }
        public async Task<GetAllQuestionTypeQueryResponse> Handle(GetAllQuestionTypeQuery request, CancellationToken cancellationToken)
        {
            var response = new GetAllQuestionTypeQueryResponse();
            try
            {
                var filter = PredicateBuilder.True<QuestionType>();
                if (!string.IsNullOrEmpty(request.Search))
                {
                    var search = request.Search.ToLower();
                    filter = filter.And(c => c.Type.ToLower().Contains(search));
                }
                var pagedResult = await _questionTypeRepository.GetPagedFilteredAsync(filter, request.Page, request.PageSize, request.SortColumn, request.SortOrder, false);

                response.Result = _mapper.Map<PagedResult<QuestionTypeResponse>>(pagedResult);
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
