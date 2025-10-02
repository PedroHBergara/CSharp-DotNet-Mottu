using Mottu.Domain.DTOs;
using Mottu.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mottu.Application.Services
{
    public interface IFuncionarioService
    {
        Task<IEnumerable<FuncionarioDto>> GetAllFuncionariosAsync(PaginationParams paginationParams = null);
        Task<FuncionarioDto> GetFuncionarioByIdAsync(int id);
        Task AddFuncionarioAsync(FuncionarioDto funcionarioDto);
        Task UpdateFuncionarioAsync(FuncionarioDto funcionarioDto);
        Task DeleteFuncionarioAsync(int id);
    }
}
