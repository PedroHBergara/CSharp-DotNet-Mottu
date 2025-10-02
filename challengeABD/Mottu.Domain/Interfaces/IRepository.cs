using System.Collections.Generic;
using System.Threading.Tasks;
using Mottu.Shared;

namespace Mottu.Domain.Interfaces
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync(PaginationParams paginationParams = null);
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
