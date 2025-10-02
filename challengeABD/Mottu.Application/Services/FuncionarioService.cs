using Mottu.Domain.DTOs;
using Mottu.Domain.Entities;
using Mottu.Domain.Interfaces.Repositories;
using Mottu.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mottu.Application.Services
{
    public class FuncionarioService : IFuncionarioService
    {
        private readonly IFuncionarioRepository _funcionarioRepository;

        public FuncionarioService(IFuncionarioRepository funcionarioRepository)
        {
            _funcionarioRepository = funcionarioRepository;
        }

        public async Task<IEnumerable<FuncionarioDto>> GetAllFuncionariosAsync(PaginationParams paginationParams = null)
        {
            var funcionarios = await _funcionarioRepository.GetAllAsync(paginationParams);
            return funcionarios.Select(f => new FuncionarioDto
            {
                Id = f.Id,
                Nome = f.Nome,
                DataNascimento = f.DataNascimento,
                Funcao = f.Funcao,
                Endereco = f.Endereco
            });
        }

        public async Task<FuncionarioDto> GetFuncionarioByIdAsync(int id)
        {
            var funcionario = await _funcionarioRepository.GetByIdAsync(id);
            if (funcionario == null) return null;

            return new FuncionarioDto
            {
                Id = funcionario.Id,
                Nome = funcionario.Nome,
                DataNascimento = funcionario.DataNascimento,
                Funcao = funcionario.Funcao,
                Endereco = funcionario.Endereco
            };
        }

        public async Task AddFuncionarioAsync(FuncionarioDto funcionarioDto)
        {
            var funcionario = new Funcionario
            {
                Nome = funcionarioDto.Nome,
                DataNascimento = funcionarioDto.DataNascimento,
                Funcao = funcionarioDto.Funcao,
                Endereco = funcionarioDto.Endereco
            };
            await _funcionarioRepository.AddAsync(funcionario);
        }

        public async Task UpdateFuncionarioAsync(FuncionarioDto funcionarioDto)
        {
            var funcionario = await _funcionarioRepository.GetByIdAsync(funcionarioDto.Id);
            if (funcionario == null) return; // Or throw an exception

            funcionario.Nome = funcionarioDto.Nome;
            funcionario.DataNascimento = funcionarioDto.DataNascimento;
            funcionario.Funcao = funcionarioDto.Funcao;
            funcionario.Endereco = funcionarioDto.Endereco;

            await _funcionarioRepository.UpdateAsync(funcionario);
        }

        public async Task DeleteFuncionarioAsync(int id)
        {
            await _funcionarioRepository.DeleteAsync(id);
        }
    }
}
