using RecruitmentProcessBusiness.QuestionTypes.Queries.GetAll;
using RecruitmentProcessDomain.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentProcessBusiness.QuestionTypes.Queries.GetIndividual
{
    public class GetQuestionTypeQueryResponse : BaseResponse
    {
        public GetQuestionTypeQueryResponse() : base()
        { }
        public QuestionTypeResponse Result { get; set; }
    }
}
