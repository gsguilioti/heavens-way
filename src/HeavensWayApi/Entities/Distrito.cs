using HeavensWayApi.Dto;

namespace HeavensWayApi.Entities
{
    public class Distrito
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public Distrito() {}
        public Distrito(DistritoDto dto)
        {
            Nome = dto.Nome;
        }

        public void MapDto(DistritoDto dto)
        {
            Nome = dto.Nome;
        }
    }
}