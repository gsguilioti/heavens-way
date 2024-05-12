using HeavensWayApi.Entities;

namespace HeavensWayApi.Dto
{
    public class TipoEventoDto
    {
        public string Descricao { get; set; }

        public TipoEventoDto(TipoEvento tipoEvento)
        {
            Descricao = tipoEvento.Descricao;
        }
    }
}
