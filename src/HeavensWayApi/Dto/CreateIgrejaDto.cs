using HeavensWayApi.Entities;

namespace HeavensWayApi.Dto
{
    public class CreateIgrejaDto
    {
        public string Nome { get; set; }
        
        public int DistritoId {get; set; }
        public string Cep {get; set; }

        public CreateIgrejaDto() {}
    }
}
