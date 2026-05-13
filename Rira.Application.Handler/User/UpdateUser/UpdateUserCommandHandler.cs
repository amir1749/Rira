using MediatR;
using Microsoft.EntityFrameworkCore;
using Rira.Core.Domain.Common.Exception;
using Rira.Utility.Framework.Common;
using Rira.Utility.Framework.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Rira.Application.Handler.User.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result>
    {
        private readonly IRepository<Core.Domain.User.Entity.User> _userRepository;


        private readonly IUnitOfWork _unitOfWork;


        public UpdateUserCommandHandler(IRepository<Core.Domain.User.Entity.User> userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByIdAsync(request.Id);

            if (user == null)
                throw new DomainException("User not found");

            user.Update(request.Id, request.FirstName, request.LastName, request.SSN, request.NationalCode);

            await _userRepository.UpdateAsync(user, request.RowVersion);


            try
            {
                await _unitOfWork.CommitAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new DomainException("Data was modified by another user. Please reload.");
            }


            return Result.Okay();
        }
    }
}
