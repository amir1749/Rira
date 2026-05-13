using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Rira.Utility.Framework.Common;
using Rira.Utility.Framework.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Rira.Utility.Framework.EntityFramework
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected IdentityDbContext<ApplicationUser> DbContext;

        protected readonly DbSet<TEntity> DbSet;

        public virtual IQueryable<TEntity> Queryable => DbSet;

        public Repository(IdentityDbContext<ApplicationUser> dbContext)
        {
            DbContext = dbContext;
            DbSet = dbContext.Set<TEntity>();
        }

        public  async Task<TEntity> FindByIdAsync(params object[]? ids)
        {
            return await DbSet.FindAsync(ids);
        }


        public async  Task AddAsync(TEntity entity,CancellationToken cancellationToken)
        {
           await DbSet.AddAsync(entity,cancellationToken);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            DbSet.AddRangeAsync(entities);
        }

        public async Task UpdateAsync(TEntity entity, byte[] rowVersion)
        {
            EntityEntry<TEntity> entityEntry = DbContext.Entry(entity);

            entityEntry.Property("RowVersion")
              .OriginalValue = rowVersion;

            if (entityEntry.State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }

            entityEntry.State = EntityState.Modified;
        }

        public  async Task UpdateFieldById<TField>(object id, Expression<Func<TEntity, TField>> memberExpression, TField value)
        {
            await UpdateFieldById(new object[1] { id }, memberExpression, value);
        }

        public  async Task UpdateFieldById<TField>(object[] ids, Expression<Func<TEntity, TField>> memberExpression, TField value)
        {
            TEntity entity = await DbSet.FindAsync(ids) ?? throw new KeyNotFoundException($"there is no item in {typeof(TEntity).Name} with id : {ids}");
            string memberName = ((MemberExpression)memberExpression.Body ?? ((UnaryExpression)memberExpression.Body).Operand as MemberExpression).Member.Name;
            entity.GetType().GetProperty(memberName)?.SetValue(entity, value);
        }

        public  async Task UpdateField<TField>(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, TField>> memberExpression, TField value)
        {
            TEntity entity = await DbSet.FirstOrDefaultAsync(filterExpression) ?? throw new KeyNotFoundException($"there is no item in {typeof(TEntity).Name} with query : {filterExpression}");
            string memberName = ((MemberExpression)memberExpression.Body ?? ((UnaryExpression)memberExpression.Body).Operand as MemberExpression).Member.Name;
            entity.GetType().GetProperty(memberName)?.SetValue(entity, value);
        }

        public async Task GenericUpdate(object id, Dictionary<string, object> fieldset)
        {
            await GenericUpdate(new object[1] { id }, fieldset);
        }

        public  async Task GenericUpdate(object[] ids, Dictionary<string, object> fieldset)
        {
            TEntity entity = await DbSet.FindAsync(ids) ?? throw new KeyNotFoundException($"there is no item in {typeof(TEntity).Name} with id : {ids}");
            foreach (KeyValuePair<string, object> field in fieldset)
            {
                entity.GetType().GetProperty(field.Key)?.SetValue(entity, field.Value);
            }
        }

        public  async Task DeleteById(params object[] id)
        {
            TEntity entity = await DbSet.FindAsync(id) ?? throw new KeyNotFoundException($"there is no item in {typeof(TEntity).Name} with id : {id}");
            DbSet.Remove(entity);
        }

        public  async Task<Result> CommitAsync(CancellationToken cancellationToken)
        {
            int updatedItems = await DbContext.SaveChangesAsync(cancellationToken);
            return updatedItems > 0 ? Result.Okay($"{updatedItems} updated on the database") : Result.Error("no change were made to the database");
        }

        public async Task Delete(TEntity entity)
        {
            EntityEntry<TEntity> entityEntry = DbContext.Entry(entity);
            if (entityEntry.State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }

            entityEntry.State = EntityState.Deleted;
        }

        public async Task DeleteManyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> entities = DbSet.Where(predicate);
            DbSet.RemoveRange(entities);
        }

       
    }
}
