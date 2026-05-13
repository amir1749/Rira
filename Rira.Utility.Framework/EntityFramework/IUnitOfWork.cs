using System;
using System.Collections.Generic;
using System.Text;

namespace Rira.Utility.Framework.EntityFramework
{
    public interface IUnitOfWork
    {
        Task CommitAsync(CancellationToken cancellationToken);
    }
}
