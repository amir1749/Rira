using Google.Protobuf.WellKnownTypes;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Rira.Application.Handler.User.CreateUser;
using Rira.Core.Domain.User.Entity;
using Rira.Infrastructure.Config;
using Rira.Infrastructure.Entityframework;
using Rira.Utility.Framework.EntityFramework;
using System;

namespace UserGrpcServiceTest
{
    public class UnitTest1
    {
        private WebApplication InitialConfig()
        {

            var builder = WebApplication.CreateBuilder();

            builder.Services.AddGrpc();

            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

            builder.Services.InitialConfig(builder.Configuration);

            var app = builder.Build();


            return app;
           
        }

        [Fact]
        public async Task Handle_Should_Create_User_Successfully()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString())
               .Options;

            var context = InitialConfig();
            var mediater = context.Services.GetRequiredService<IMediator>();
            var unitOfWork = context.Services.GetRequiredService<IUnitOfWork>();
            var httpContextAccessor = context.Services.GetRequiredService<IHttpContextAccessor>();
            var userRepo = context.Services.GetRequiredService<IRepository<Rira.Core.Domain.User.Entity.User>>();
            var dbContext= new ApplicationDbContext(options, mediater, httpContextAccessor);

            // Arrange

         

            var handler = new CreateUserCommandHandler(userRepo, unitOfWork);

            var command = new CreateUserCommand(
                "Amir",
                "Azad",
                "11111",
                "1212121212"
            );

            // Act
            var userId = await handler.Handle(command, CancellationToken.None);

            // Assert
            var user = await dbContext.User
                .FirstOrDefaultAsync(x => x.Id == userId.Data);

            Assert.NotNull(user);
            Assert.Equal("Amir", user.FirstName);
        }
    }
}
