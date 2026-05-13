using MediatR;
using Rira.Core.Domain.Common.Exception;
using Rira.Core.Domain.User.Event;
using Rira.Utility.Framework.Common;
using Rira.Utility.Framework.EntityFramework;
using System;
using System.Collections.Generic;
using System.Runtime.Intrinsics.X86;
using System.Text;

namespace Rira.Application.Handler.User.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result>
    {

        private readonly IRepository<Core.Domain.User.Entity.User> _userRepository;


        private readonly IUnitOfWork _unitOfWork;

        public DeleteUserCommandHandler(IRepository<Core.Domain.User.Entity.User> userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user= await _userRepository.FindByIdAsync(request.Id);

            if (user == null)
                throw new DomainException("User not found");

            user.AddDomainEvent(new UserDeletedEvent(request.Id));

            await _userRepository.Delete(user);

            await _userRepository.CommitAsync(cancellationToken);

            return Result.Okay();

        }
    }
}
