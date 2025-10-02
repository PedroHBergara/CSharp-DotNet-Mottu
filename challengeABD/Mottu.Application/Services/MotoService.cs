using Mottu.Domain.DTOs;
using Mottu.Domain.Entities;
using Mottu.Domain.Interfaces.Repositories;
using Mottu.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mottu.Application.Services
{
    public class MotoService : IMotoService
    {
        private readonly IMotoRepository _motoRepository;

        public MotoService(IMotoRepository motoRepository)
        {
            _motoRepository = motoRepository;
        }

        public async Task<IEnumerable<MotoDto>> GetAllMotosAsync(PaginationParams paginationParams = null)
        {
            var motos = await _motoRepository.GetAllAsync(paginationParams);
            return motos.Select(m => new MotoDto
            {
                Id = m.Id,
                Modelo = m.Modelo,
                Status = m.Status,
                Placa = m.Placa
            });
        }

        public async Task<MotoDto> GetMotoByIdAsync(int id)
        {
            var moto = await _motoRepository.GetByIdAsync(id);
            if (moto == null) return null;

            return new MotoDto
            {
                Id = moto.Id,
                Modelo = moto.Modelo,
                Status = moto.Status,
                Placa = moto.Placa
            };
        }

        public async Task AddMotoAsync(MotoDto motoDto)
        {
            var moto = new Moto
            {
                Modelo = motoDto.Modelo,
                Status = motoDto.Status,
                Placa = motoDto.Placa
            };
            await _motoRepository.AddAsync(moto);
        }

        public async Task UpdateMotoAsync(MotoDto motoDto)
        {
            var moto = await _motoRepository.GetByIdAsync(motoDto.Id);
            if (moto == null) return; // Or throw an exception

            moto.Modelo = motoDto.Modelo;
            moto.Status = motoDto.Status;
            moto.Placa = motoDto.Placa;

            await _motoRepository.UpdateAsync(moto);
        }

        public async Task DeleteMotoAsync(int id)
        {
            await _motoRepository.DeleteAsync(id);
        }
    }
}
