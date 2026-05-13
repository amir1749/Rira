using Rira.Utility.Framework.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Rira.Utility.Framework.EntityFramework
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Queryable { get; }

        Task<TEntity> FindByIdAsync(params object[]? ids);


        Task AddAsync(TEntity entity,CancellationToken cancellationToken);

        Task AddRangeAsync(IEnumerable<TEntity> entities);

        Task UpdateAsync(TEntity entity ,byte[] rowVersion);

        Task UpdateFieldById<TField>(object id, Expression<Func<TEntity, TField>> memberExpression, TField value);

        Task UpdateFieldById<TField>(object[] ids, Expression<Func<TEntity, TField>> memberExpression, TField value);

        Task UpdateField<TField>(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, TField>> memberExpression, TField value);

        Task GenericUpdate(object id, Dictionary<string, object> fieldset);

        Task GenericUpdate(object[] ids, Dictionary<string, object> fieldset);

        Task DeleteById(params object[] id);

        Task Delete(TEntity entity);

        Task DeleteManyAsync(Expression<Func<TEntity, bool>> predicate);

        Task<Result> CommitAsync(CancellationToken cancellationToken);
    }
}
