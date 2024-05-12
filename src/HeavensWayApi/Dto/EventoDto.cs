using HeavensWayApi.Entities;

namespace HeavensWayApi.Dto
{
    public class EventoDto
    {
        public string Descricao { get; set; }

        public int TipoEventoId { get; set; }

        public DateTime DataInicio { get; set; }
        public TimeSpan Duracao { get; set; }

        public EventoDto(Evento evento)
        {
            Descricao = evento.Descricao;
            TipoEventoId = evento.TipoEventoId;
            DataInicio = evento.DataInicio;
            Duracao = evento.Duracao;
        }
    }
}
