using MediatR;
using Rira.Application.DTO;
using Rira.Utility.Framework.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rira.Application.Handler.User.GetUser
{
    public class GetAllUserQuery:IRequest<Result<List<UserDTO>>>
    {

    }
}
