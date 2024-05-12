using HeavensWayApi.Dto;

namespace HeavensWayApi.Entities
{
    public class Endereco
    {
        public int Id { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }

        public Endereco() {}

        public Endereco(EnderecoDto dto)
        {
            Cep = dto.Cep;
            Logradouro = dto.Logradouro;
            Bairro = dto.Bairro;
            Cidade = dto.Cidade;
            Estado = dto.Estado;
        }

        public void MapDto(EnderecoDto dto)
        {
            Cep = dto.Cep;
            Logradouro = dto.Logradouro;
            Bairro = dto.Bairro;
            Cidade = dto.Cidade;
            Estado = dto.Estado;
        }
    }
}