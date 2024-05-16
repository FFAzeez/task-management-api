using AutoMapper;
using MediatR;
using RecruitmentProcessDomain.BindingModels;
using RecruitmentProcessDomain.Models;
using RecruitmentProcessInfrastructure.Persistence.Context;
using RecruitmentProcessInfrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentProcessBusiness.Applications.Commands.Create
{
    public class CreateApplicationFormCommandHandler : IRequestHandler<CreateApplicationFormCommand, CreateApplicationFormCommandResponse>
    {
        private readonly IAsyncRepository<ApplicationForm> _applicationFormRepository;
        private readonly IMapper _mapper;
        public CreateApplicationFormCommandHandler(IAsyncRepository<ApplicationForm> applicationFormRepo, IMapper mapper)
        {
            _applicationFormRepository = applicationFormRepo;
            _mapper = mapper;
        }

        public  async Task<CreateApplicationFormCommandResponse> Handle(CreateApplicationFormCommand request, CancellationToken cancellationToken)
        {
            var response = new CreateApplicationFormCommandResponse();
            try
            {
                var validate = new CreateApplicationFormCommandValidator();
                var validateResult = await validate.ValidateAsync(request);
                if (validateResult.Errors.Any()) throw new Exception(string.Join(Environment.NewLine,validateResult));
                var application = _mapper.Map<ApplicationForm>(request);
                await _applicationFormRepository.AddAsync(application);
                response.Success = true;
                response.Message = Constant.SuccessCreated;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
