using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentProcessBusiness.QuestionTypes.Commands.Create
{
    public class CreateQuestionTypeCommandValidator : AbstractValidator<CreateQuestionTypeCommand>
    {
        public CreateQuestionTypeCommandValidator()
        {
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
