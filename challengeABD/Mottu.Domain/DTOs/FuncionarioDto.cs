using System;
using Mottu.Shared;

namespace Mottu.Domain.DTOs
{
    public class FuncionarioDto : Resource
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Funcao { get; set; }
        public string? Endereco { get; set; }
    }
}
