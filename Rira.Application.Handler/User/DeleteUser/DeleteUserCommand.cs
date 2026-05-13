using MediatR;
using Rira.Utility.Framework.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rira.Application.Handler.User.DeleteUser
{

    public sealed record DeleteUserCommand(Guid? Id) : IRequest<Result>;
}
