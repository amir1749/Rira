using Azure;
using Google.Protobuf;
using Grpc.Core;
using MediatR;
using Rira.Application.DTO;
using Rira.Application.Handler.User.CreateUser;
using Rira.Application.Handler.User.DeleteUser;
using Rira.Application.Handler.User.GetUser;
using Rira.Application.Handler.User.UpdateUser;
using Rira.Core.Domain.User.Entity;
using Rira.Grpc;

namespace Rira.GRPCS.Services
{
    public class UserGrpcService : UserService.UserServiceBase
    {
        private readonly Mediator _mediator;

        public UserGrpcService(Mediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task<CreateUserResponse> CreateUser(CreateUserRequest request, ServerCallContext context)
        {
            var command = new CreateUserCommand(
                   request.FirstName,
                   request.LastName,
                   request.Ssn,
                   request.NationalCode);

            var result = await _mediator.Send(command);

            return new CreateUserResponse
            {
                Id = result.Data.ToString()
            };
        }

        public override async Task<GetAllUserResponse> GetAllUser(GetAllUserRequest request,ServerCallContext context)
        {
            var query = new GetAllUserQuery();

            var users = await _mediator.Send(query);

            var response = new GetAllUserResponse();

            response.Users.AddRange(users.Data.Select(x => new UserResponse
            {
                Id = x.Id.ToString(),
                FirstName = x.FirstName,
                LastName = x.LastName,
                Ssn = x.SSN,
                NationalCode = x.NationalCode
            }).ToList());

            return response;
        }

        public override async Task<UserResponse> GetUserById(GetUserByIdRequest request, ServerCallContext context)
        {
            var query = new GetUserByIdQuery(Guid.Parse(request.Id));

            var user = await _mediator.Send(query);

            var response = new UserResponse
            {
                Id = user.Data.Id.ToString(),
                FirstName = user.Data.FirstName,
                LastName = user.Data.LastName,
                Ssn = user.Data.SSN,
                NationalCode = user.Data.NationalCode,
                RowVersion = ByteString.CopyFrom(user.Data.RowVersion)
            };

            return response;
        }

        public override async Task<UpdateUserResponse> UpdateUser(UpdateUserRequest request,ServerCallContext context)
        {
            var command = new UpdateUserCommand(
                Guid.Parse(request.Id),
                request.FirstName,
                request.LastName,
                request.Ssn,
                request.NationalCode,
                request.RowVersion.ToByteArray());

            var result = await _mediator.Send(command);

            return new UpdateUserResponse
            {
                Success = result
            };
        }

        public override async Task<DeleteUserResponse> DeleteUser(DeleteUserRequest request,ServerCallContext context)
        {
            var command = new DeleteUserCommand(
                Guid.Parse(request.Id));

            var result = await _mediator.Send(command);

            return new DeleteUserResponse
            {
                Success = result
            };
        }
    }
}
