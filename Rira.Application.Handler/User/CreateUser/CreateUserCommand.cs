using MediatR;
using Rira.Utility.Framework.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rira.Application.Handler.User.CreateUser;

public sealed record CreateUserCommand(
string? FirstName,
string? LastName,
string? SSN,
string? NationalCode) : IRequest<Result<Guid>>;

