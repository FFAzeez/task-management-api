using MediatR;
using RecruitmentProcessDomain.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentProcessBusiness.QuestionTypes.Queries.GetAll
{
    public class GetAllQuestionTypeQuery : PagingModel, IRequest<GetAllQuestionTypeQueryResponse>
    {
        public string? Search { get; set; }
    }
}
