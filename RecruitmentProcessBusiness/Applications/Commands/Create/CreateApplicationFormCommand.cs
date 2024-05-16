using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentProcessBusiness.Applications.Commands.Create
{
    public class CreateApplicationFormCommand:IRequest<CreateApplicationFormCommandResponse>
    {
        public string ProgramDetailId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public InternalHideDataCommand DOB { get; set; }
        public InternalHideDataCommand Gender { get; set; }
        public InternalHideDataCommand IDNumber { get; set; }
        public InternalHideDataCommand PhoneNumber { get; set; }
        public InternalHideDataCommand Nationality { get; set; }
        public InternalHideDataCommand CurrentResidence { get; set; }
        public List<AddQuestionCommand> AddQuestions { get; set; }
    }

    public class InternalHideDataCommand
    {
        public string Value { get; set; }
        public bool Hide { get; set; }
        public bool Internal { get; set; }
    }

    public class AddQuestionCommand
    {
        public string Type { get; set; }
        public string Question { get; set; }
        public List<ChoiceDataCommand>? Choices { get; set; }
        public bool? EnableOtherOption { get; set; }
    }
    public class ChoiceDataCommand
    {
        public string Choice { get; set; }
    }
}
