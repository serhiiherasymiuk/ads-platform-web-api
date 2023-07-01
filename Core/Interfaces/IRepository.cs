using Ardalis.Specification;

namespace Core.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();

        Task<TEntity?> GetByID(object id);

        Task<TEntity?> GetBySpec(ISpecification<TEntity> specification);
        Task<IEnumerable<TEntity>> GetAllBySpec(ISpecification<TEntity> specification);

        Task Insert(TEntity entity);

        Task Delete(object id);

        Task Delete(TEntity entityToDelete);

        Task Update(TEntity entityToUpdate);

        Task Save();
    }
}
