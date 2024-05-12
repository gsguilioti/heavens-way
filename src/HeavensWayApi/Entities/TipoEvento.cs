using HeavensWayApi.Dto;

namespace HeavensWayApi.Entities
{
    public class TipoEvento
    {
        public int Id {get; set; }
        public string Descricao { get; set; }

        public TipoEvento() {}

        public TipoEvento(TipoEventoDto dto)
        {
            Descricao = dto.Descricao;
        }

        public void MapDto(TipoEventoDto dto)
        {
            Descricao = dto.Descricao;
        }
    }
}
