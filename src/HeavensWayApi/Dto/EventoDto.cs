using HeavensWayApi.Entities;

namespace HeavensWayApi.Dto
{
    public class EventoDto
    {
        public string Descricao { get; set; }
        public int TipoEventoId { get; set; }
        public int IgrejaId { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

        public EventoDto() {}
        public EventoDto(Evento evento)
        {
            Descricao = evento.Descricao;
            TipoEventoId = evento.TipoEventoId;
            DataInicio = evento.DataInicio;
            DataFim = evento.DataFim;
        }
    }
}
