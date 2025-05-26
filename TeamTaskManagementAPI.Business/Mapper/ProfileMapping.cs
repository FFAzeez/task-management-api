using AutoMapper;
using TeamTaskManagementAPI.Domain.Models;
using TeamTaskManagementAPI.Domain.BindingModels.Response;

namespace TeamTaskManagementAPI.Business.Mapper
{
    public class ProfileMapping:Profile
    {
        public ProfileMapping()
        {
            //CreateMap<PagedResult<QuestionType>, PagedResult<QuestionTypeResponse>>();
            CreateMap<TaskModel, TaskResponse>();
            // CreateMap<CreateApplicationFormCommand,ApplicationForm>();
            // CreateMap<InternalHideDataCommand, InternalHideData>();
            // CreateMap<AddQuestionCommand, AddQuestion>();
            // CreateMap<ChoiceDataCommand, ChoiceData>();
        }
    }
}
