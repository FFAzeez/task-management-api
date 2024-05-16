using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentProcessBusiness.Applications.Commands.Create
{
    public class CreateApplicationFormCommandValidator:AbstractValidator<CreateApplicationFormCommand>
    {
        public CreateApplicationFormCommandValidator()
        {
            RuleFor(x => x.ProgramDetailId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .NotNull()
                .NotEqual("string")
                .WithMessage("ProgramDetail is required and can't be equal to string.");
            RuleFor(x=>x.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .NotNull()
                .NotEqual("string")
                .WithMessage("FirstName is required and can't be equal to string.");
            RuleFor(x => x.LastName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .NotNull()
                .NotEqual("string")
                .WithMessage("LastName is required and can't be equal to string.");
            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .NotNull()
                .NotEqual("string")
                .WithMessage("Email is required and can't be equal to string.");
        }
    }
}
