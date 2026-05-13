using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rira.Application.Handler.User.DeleteUser
{
    public class DeleteUserValidator:AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
