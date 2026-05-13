using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rira.Core.Domain.User.Entity;
using Rira.Infrastructure.Entityframework.Configuration;
using Rira.Utility.Framework.Domain;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Rira.Infrastructure.Entityframework
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        private readonly IMediator _mediator;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options, IMediator mediator, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new UserConfig());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            Guid _userId = Guid.NewGuid();

            var addedEntries = ChangeTracker.Entries().Where(e => e.State == EntityState.Added).ToList();
            var modifiedEntries = ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted || e.State == EntityState.Modified).ToList();

            if (_httpContextAccessor.HttpContext.User != null && _httpContextAccessor.HttpContext.User.Identity != null &&
                _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                Guid.TryParse((_httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity).FindFirst(ClaimTypes.NameIdentifier).Value, out _userId);
            }

            foreach (var entry in modifiedEntries)
            {
                var entity = entry.Entity as BaseAuditEntity;
                if (entity != null && entry.Entity.GetType().GetProperty("ModifierId") != null)
                {
                    entity.ModifierId = _userId;
                }
                if (entity != null && entry.Entity.GetType().GetProperty("ModifyDatetime") != null)
                {
                    entity.ModifyDatetime = DateTime.Now;
                }
            }
            foreach (var entry in addedEntries)
            {
                var entity = entry.Entity as BaseAuditEntity;
                if (entity != null)
                {
                    entity.Id = Guid.NewGuid();
                }
                if (entity != null && entry.Entity.GetType().GetProperty("CreatorId") != null)
                {
                    entity.CreatorId = _userId;
                }
                if (entity != null && entry.Entity.GetType().GetProperty("CreateDateTime") != null)
                {
                    entity.CreateDateTime = DateTime.Now;
                }
            }

            try
            {
                var response = await base.SaveChangesAsync();
                _dispatchDomainEvents().GetAwaiter().GetResult();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task _dispatchDomainEvents()
        {
            var domainEventEntities = ChangeTracker.Entries<BaseAuditEntity>()
                .Select(po => po.Entity)
                .Where(po => po.DomainEvents.Any())
                .ToArray();

            foreach (var entity in domainEventEntities)
            {
                var events = entity.DomainEvents.ToArray();
                entity.DomainEvents.Clear();
                foreach (var entityDomainEvent in events)
                    await _mediator.Publish(entityDomainEvent);
            }
        }
    }
}
