using Mottu.Domain.DTOs;
using Mottu.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mottu.Application.Services
{
    public interface IMotoService
    {
        Task<IEnumerable<MotoDto>> GetAllMotosAsync(PaginationParams paginationParams = null);
        Task<MotoDto> GetMotoByIdAsync(int id);
        Task AddMotoAsync(MotoDto motoDto);
        Task UpdateMotoAsync(MotoDto motoDto);
        Task DeleteMotoAsync(int id);
    }
}
