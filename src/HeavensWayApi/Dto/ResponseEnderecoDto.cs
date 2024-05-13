using HeavensWayApi.Entities;

namespace HeavensWayApi.Dto
{
    public class ResponseEnderecoDto
    {
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string complemento { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string Uf { get; set; }
        public string Ibge { get; set; }

        public ResponseEnderecoDto() {}
        public ResponseEnderecoDto(Endereco endereco)
        {
            Cep = endereco.Cep;
            Logradouro = endereco.Logradouro;
            Bairro = endereco.Bairro;
            Localidade = endereco.Cidade;
            Uf = endereco.Estado;
        }
    }
}
