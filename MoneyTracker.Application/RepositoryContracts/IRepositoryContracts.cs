using MoneyTracker.Application.DTOs;
using MoneyTracker.Domain.Entities;
using System.Linq.Expressions;
using static MoneyTracker.Domain.Enums.SD;
namespace MoneyTracker.Application.RepositoryContracts
{
    /// <summary>
    /// Repersents data access logic for managing General Entity
    /// </summary>
    public interface IRepositoryContracts<T> where T : class
    {
        // Read operations
        Task<T?> Get(Expression<Func<T, bool>> filter = null, string? includeProp = null);
        Task<List<T>> GetAll(Expression<Func<T, bool>> filter = null, string? includeProp =null);
        // Create operations
        Task<T> Create(T entity);
        // Delete operations
        Task<bool> Delete(T entity);
        Task<bool> DeleteRange(IEnumerable<T> entities);

    }
}
