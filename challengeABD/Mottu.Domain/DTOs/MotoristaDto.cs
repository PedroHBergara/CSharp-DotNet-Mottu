using System;
using Mottu.Shared;

namespace Mottu.Domain.DTOs
{
    public class MotoristaDto : Resource
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public int? UltimaMotoUsadaId { get; set; }
        public string? Endereco { get; set; }
    }
}
