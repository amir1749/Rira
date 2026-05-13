using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Rira.Utility.Framework.Common;
using Rira.Utility.Framework.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rira.Application.Handler.User.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<Guid>>
{

    private readonly IRepository<Core.Domain.User.Entity.User> _userRepository;


    private readonly IUnitOfWork _unitOfWork;


    public CreateUserCommandHandler(IRepository<Core.Domain.User.Entity.User> userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new Core.Domain.User.Entity.User(
          request.FirstName,
          request.LastName,
          request.SSN,
          request.NationalCode);

        await _userRepository.AddAsync(user, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        return Result<Guid>.Okay(user.Id);
    }
}

