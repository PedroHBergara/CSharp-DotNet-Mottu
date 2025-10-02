namespace Mottu.Domain.Entities
{
    public class Motorista
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public int? UltimaMotoUsadaId { get; set; } // Foreign key to Moto
        public string? Endereco { get; set; }

        // Navigation property
        public Moto? UltimaMotoUsada { get; set; }
    }
}
