namespace PT.Application.Interfaces.Repositories;

public interface IBaseRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task AddAsync(T entity, CancellationToken ct = default);
    Task Update(T entity);
    Task SaveChangesAsync(CancellationToken ct = default);
}
   