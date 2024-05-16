using AutoMapper;
using RecruitmentProcessBusiness.Applications.Commands.Create;
using RecruitmentProcessBusiness.Applications.Queries.GetAll;
using RecruitmentProcessBusiness.QuestionTypes.Queries.GetAll;
using RecruitmentProcessDomain.BindingModels;
using RecruitmentProcessDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentProcessBusiness.Mapper
{
    public class ProfileMapping:Profile
    {
        public ProfileMapping()
        {
            CreateMap<PagedResult<QuestionType>, PagedResult<QuestionTypeResponse>>();
            CreateMap<QuestionType, QuestionTypeResponse>();
            CreateMap<CreateApplicationFormCommand,ApplicationForm>();
            CreateMap<InternalHideDataCommand, InternalHideData>();
            CreateMap<AddQuestionCommand, AddQuestion>();
            CreateMap<ChoiceDataCommand, ChoiceData>();
        }
    }
}
