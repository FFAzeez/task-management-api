using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentProcessBusiness.QuestionTypes.Commands.Create
{
    public class CreateQuestionTypeCommand : IRequest<CreateQuestionTypeCommandResponse>
    {
        public string Type { get; set; }
    }
}
