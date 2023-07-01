using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        internal AdsPlatformDbContext context;
        internal DbSet<TEntity> dbSet;

        public Repository(AdsPlatformDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public async virtual Task<IEnumerable<TEntity>> GetAll()
        {
            return await dbSet.ToListAsync();
        }

        public async virtual Task<TEntity?> GetByID(object id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<TEntity?> GetBySpec(ISpecification<TEntity> specification)
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllBySpec(ISpecification<TEntity> specification)
        {
            return await ApplySpecification(specification).ToListAsync();
        }

        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification)
        {
            var evaculator = new SpecificationEvaluator();
            return evaculator.GetQuery(dbSet, specification);
        }

        public async virtual Task Insert(TEntity entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async virtual Task Delete(object id)
        {
            TEntity? entityToDelete = await dbSet.FindAsync(id);
            if (entityToDelete != null)
                await Delete(entityToDelete);
        }

        public virtual Task Delete(TEntity entityToDelete)
        {
            return Task.Run(() =>
            {
                if (context.Entry(entityToDelete).State == EntityState.Detached)
                {
                    dbSet.Attach(entityToDelete);
                }
                dbSet.Remove(entityToDelete);
            });
        }

        public virtual Task Update(TEntity entityToUpdate)
        {
            return Task.Run(() =>
            {
                dbSet.Attach(entityToUpdate);
                context.Entry(entityToUpdate).State = EntityState.Modified;
            });
        }
    }
}