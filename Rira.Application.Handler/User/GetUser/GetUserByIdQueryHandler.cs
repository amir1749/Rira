using MediatR;
using Rira.Application.DTO;
using Rira.Utility.Framework.Common;
using Rira.Utility.Framework.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rira.Application.Handler.User.GetUser
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserDTO>>
    {
         private readonly IRepository<Core.Domain.User.Entity.User> _userRepository;

        public GetUserByIdQueryHandler(IRepository<Core.Domain.User.Entity.User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<UserDTO>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
           var result=await _userRepository.FindByIdAsync(request.id);

            return new Result<UserDTO>
            {
                Data = result.ToDto(),
                Succeed = true
            };
        }
    }
}
