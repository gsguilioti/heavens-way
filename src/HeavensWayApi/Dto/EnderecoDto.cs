using HeavensWayApi.Entities;

namespace HeavensWayApi.Dto
{
    public class EnderecoDto
    {
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }

        public EnderecoDto() {}
        public EnderecoDto(Endereco endereco)
        {
            Cep = endereco.Cep;
            Logradouro = endereco.Logradouro;
            Bairro = endereco.Bairro;
            Cidade = endereco.Cidade;
            Estado = endereco.Estado;
        }
    }
}
