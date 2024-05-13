using HeavensWayApi.Repositories;
using HeavensWayApi.Dto;
using HeavensWayApi.Entities;
using System.Text.Json;

namespace HeavensWayApi.Services
{
    public class EnderecoService
    {
        private readonly EnderecoRepository _enderecoRepository;
        private readonly HttpClient _httpClient;

        const string URI = "https://opencep.com/v1/";

        public EnderecoService(EnderecoRepository enderecoRepository)
        {
            _enderecoRepository = enderecoRepository;

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(URI);
        }

        public async Task<Endereco> Create(string cep)
        { 
            var endereco = _enderecoRepository.GetByCep(cep);
            if(endereco != null)
                return endereco;

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{cep}");

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content. ReadAsStringAsync();
                    ResponseEnderecoDto responseDto = JsonSerializer.Deserialize<ResponseEnderecoDto>(responseBody);
                    endereco.MapResponseDto(responseDto);
                    _enderecoRepository.Create(endereco);
                }
            }
            catch(HttpRequestException ex)
            {
                throw;
            }

            return endereco;
        }
    }
}