using MediatR;
using Rira.Utility.Framework.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rira.Application.Handler.User.UpdateUser
{

    public sealed record UpdateUserCommand(
    Guid? Id,
    string? FirstName,
    string? LastName,
    string? SSN,
    string? NationalCode,
    byte[] RowVersion) : IRequest<Result>;

}
