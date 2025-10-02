using Microsoft.EntityFrameworkCore;
using Mottu.Domain.Interfaces;
using Mottu.Infrastructure.Data;
using Mottu.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mottu.Infrastructure.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync(PaginationParams paginationParams = null)
        {
            var query = _context.Set<T>().AsQueryable();

            if (paginationParams != null)
            {
                query = query.Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                             .Take(paginationParams.PageSize);
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
