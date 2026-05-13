using MediatR;
using Microsoft.EntityFrameworkCore;
using Rira.Application.DTO;
using Rira.Utility.Framework.Common;
using Rira.Utility.Framework.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rira.Application.Handler.User.GetUser
{
    public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, Result<List<UserDTO>>>
    {

        private readonly IRepository<Core.Domain.User.Entity.User> _userRepository;

        public GetAllUserQueryHandler(IRepository<Core.Domain.User.Entity.User> userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Result<List<UserDTO>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.Queryable.ToListAsync(cancellationToken);

            return new Result<List<UserDTO>>
            {
                Data = users.ToDtos(),
                Succeed = true,
            };

        }

    }
}
