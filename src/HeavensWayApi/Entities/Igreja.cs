using HeavensWayApi.Dto;

namespace HeavensWayApi.Entities
{
    public class Igreja
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        
        public int DistritoId {get; set; }
        public Distrito Distrito { get; set; }

        public int EnderecoId {get; set; }
        public Endereco Endereco { get; set; }

        public ICollection<Evento> Eventos { get; set; }

        public Igreja() {}

        public Igreja(IgrejaDto dto)
        {
            Nome = dto.Nome;
            DistritoId = dto.DistritoId;
            EnderecoId = dto.EnderecoId;
        }

        public Igreja(CreateIgrejaDto dto, int enderecoId)
        {
            Nome = dto.Nome;
            DistritoId = dto.DistritoId;
            EnderecoId = enderecoId;
        }

        public void MapDto(IgrejaDto dto)
        {
            Nome = dto.Nome;
            DistritoId = dto.DistritoId;
            EnderecoId = dto.EnderecoId;
        }
    }
}