using HeavensWayApi.Entities;

namespace HeavensWayApi.Dto
{
    public class DistritoDto
    {
        public string Nome { get; set; }

        public DistritoDto() {}
        public DistritoDto(Distrito distrito)
        {
            Nome = distrito.Nome;
        }
    }
}
