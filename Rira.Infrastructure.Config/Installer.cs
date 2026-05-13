using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Rira.Application.Handler.Common;
using Rira.Application.Handler.User.CreateUser;
using Rira.Infrastructure.Entityframework;
using Rira.Utility.Framework.Domain;
using Rira.Utility.Framework.EntityFramework;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Rira.Infrastructure.Config
{
    public static class Installer
    {
        public static readonly ILoggerFactory LogFactory =
        LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
        });

        public static void InitialConfig(this IServiceCollection services, IConfiguration configuration)
        {
            ConfigDatabase(services, configuration);
            ConfigServiceBus(services);
            ConfigureInfrastructure(services);
            ConfigIdentity(services);
            ConfigValidation(services);
        }


        private static void ConfigDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentityDbContext<ApplicationUser>, ApplicationDbContext>(options =>
            {
                options.UseLoggerFactory(LogFactory);
                options.EnableSensitiveDataLogging();
                options.UseSqlServer(configuration.GetConnectionString("CommandDbConnection"),
                    c => c.MigrationsAssembly("Rira.Grpc"));
            });

        }


        private static void ConfigValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        }

   

        private static void ConfigServiceBus(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateUserCommandHandler).Assembly));
        }

        private static void ConfigureInfrastructure(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddHttpContextAccessor();
        }

        private static void ConfigIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        }

     

    }
}
