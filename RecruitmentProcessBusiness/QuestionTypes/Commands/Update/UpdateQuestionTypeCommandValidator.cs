using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentProcessBusiness.QuestionTypes.Commands.Update
{
    public class UpdateQuestionTypeCommandValidator : AbstractValidator<UpdateQuestionTypeCommand>
    {
        public UpdateQuestionTypeCommandValidator()
        {
            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .NotNull()
                .NotEqual("string")
                .WithMessage("Id is required and can't be equal to string.");
            RuleFor(x => x.Title)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .NotNull()
                .NotEqual("string")
                .WithMessage("Title is required and can't be equal to string.");
            RuleFor(x => x.Description)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .NotNull()
                .NotEqual("string")
                .WithMessage("Description is required and can't be equal to string.");
        }
    }
}
