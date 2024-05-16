using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentProcessDomain.Models
{
    public class ApplicationForm : BaseModel
    {
        public string ProgramDetailId { get; set; }
        //public ProgramDetail ProgramDetail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public  InternalHideData DOB { get; set; }
        public InternalHideData Gender { get; set; }
        public InternalHideData IDNumber { get; set; }
        public InternalHideData PhoneNumber { get; set; }
        public InternalHideData Nationality { get; set; }
        public InternalHideData CurrentResidence { get; set; }
        public List<AddQuestion> AddQuestions { get; set; }
    }

    public class InternalHideData
    {
        public string Value { get; set; }
        public bool Hide { get; set; }
        public bool Internal { get; set; }
    }

    public class AddQuestion
    {
        public string Type { get; set; }
        public string Question { get; set; }
        public List<ChoiceData>? Choices { get; set; }
        public bool? EnableOtherOption { get; set; }
    }
    public class ChoiceData
    {
        public string Choice { get; set; }
    }
}
