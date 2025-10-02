using Mottu.Domain.DTOs;
using Mottu.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mottu.Application.Services
{
    public interface IMotoristaService
    {
        Task<IEnumerable<MotoristaDto>> GetAllMotoristasAsync(PaginationParams paginationParams = null);
        Task<MotoristaDto> GetMotoristaByIdAsync(int id);
        Task AddMotoristaAsync(MotoristaDto motoristaDto);
        Task UpdateMotoristaAsync(MotoristaDto motoristaDto);
        Task DeleteMotoristaAsync(int id);
    }
}
