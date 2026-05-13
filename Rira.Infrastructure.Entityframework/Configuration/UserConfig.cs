using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rira.Core.Domain.User.Entity;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace Rira.Infrastructure.Entityframework.Configuration
{
    public class UserConfig : IEntityTypeConfiguration<Rira.Core.Domain.User.Entity.User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.RowVersion)
               .IsRowVersion();

            builder.Property(x => x.FirstName).HasMaxLength(32);

            builder.Property(x => x.LastName).HasMaxLength(64);

            builder.Property(x => x.SSN).HasMaxLength(10);

            builder.Property(x=>x.NationalCode).HasMaxLength(10);

        }
    }
}
