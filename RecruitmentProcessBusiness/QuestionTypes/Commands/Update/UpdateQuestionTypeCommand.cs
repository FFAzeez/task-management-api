using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentProcessBusiness.QuestionTypes.Commands.Update
{
    public class UpdateQuestionTypeCommand : IRequest<UpdateQuestionTypeCommandResponse>
    {
        public string Id { get; set; }
        public string Type { get; set; }
    }
}
