using RecruitmentProcessDomain.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentProcessBusiness.QuestionTypes.Queries.GetAll
{
    public class GetAllQuestionTypeQueryResponse : BaseResponse
    {
        public GetAllQuestionTypeQueryResponse() : base()
        {
        }
        public PagedResult<QuestionTypeResponse> Result { get; set; }
    }
}
