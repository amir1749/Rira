using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Rira.Utility.Framework.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rira.Utility.Framework.EntityFramework
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private IdentityDbContext<ApplicationUser> _context;

        public UnitOfWork(IdentityDbContext<ApplicationUser> context)
        {
            _context = context;
        }

        public async Task CommitAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _context.Dispose();

        }
    }
}
