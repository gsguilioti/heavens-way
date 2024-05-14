using HeavensWayApi.Dto;

namespace HeavensWayApi.Entities
{
    public class Evento
    {
        public int Id { get; set; }
        public string Descricao { get; set; }

        public int TipoEventoId { get; set; }
        public TipoEvento TipoEvento { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public ICollection<Usuario> Usuarios { get; set; }
        public ICollection<Igreja> Igrejas { get; set; }

        public Evento() {}

        public Evento(EventoDto dto)
        {
            Descricao = dto.Descricao;
            TipoEventoId = dto.TipoEventoId;
            DataInicio = dto.DataInicio;
            DataFim = dto.DataFim;
        }

        public void MapDto(EventoDto dto)
        {
            Descricao = dto.Descricao;
            TipoEventoId = dto.TipoEventoId;
            DataInicio = dto.DataInicio;
            DataFim = dto.DataFim;
        }
    }
}