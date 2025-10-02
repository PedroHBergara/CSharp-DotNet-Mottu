using Mottu.Shared;

namespace Mottu.Domain.DTOs
{
    public class MotoDto : Resource
    {
        public int Id { get; set; }
        public string Modelo { get; set; }
        public bool Status { get; set; }
        public string Placa { get; set; }
    }
}
