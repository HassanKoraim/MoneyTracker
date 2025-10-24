using MoneyTracker_API.DTOs;
using MoneyTracker_API.Models;
using System.Linq.Expressions;
using static MoneyTracker_Utility.SD;

namespace MoneyTracker_API.RepositoryContracts
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

        // Update operations  
        Task<T?> Update(T entity); 

        // Delete operations
        Task<bool> Delete(T entity); 

    }
}
