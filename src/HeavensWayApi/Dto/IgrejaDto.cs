using HeavensWayApi.Entities;

namespace HeavensWayApi.Dto
{
    public class IgrejaDto
    {
        public string Nome { get; set; }
        
        public int DistritoId {get; set; }
        public int EnderecoId {get; set; }

        public IgrejaDto(Igreja igreja)
        {
            Nome = igreja.Nome;
            DistritoId = igreja.DistritoId;
            EnderecoId = igreja.EnderecoId;
        }
    }
}
