using MediatR;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentProcessBusiness.QuestionTypes.Queries.GetIndividual
{
    public class GetQuestionTypeQuery : IRequest<GetQuestionTypeQueryResponse>
    {
        public string Id { get; set; }
    }
}
