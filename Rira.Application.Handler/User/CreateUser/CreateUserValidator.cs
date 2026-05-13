using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rira.Application.Handler.User.CreateUser
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(100);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(100);

            RuleFor(x => x.SSN)
                .NotEmpty()
                .Length(9)
                .Matches("^[0-9]+$");

            RuleFor(x => x.NationalCode)
                .NotEmpty()
                .Length(10)
                .Matches("^[0-9]+$");
        }
    }
}
