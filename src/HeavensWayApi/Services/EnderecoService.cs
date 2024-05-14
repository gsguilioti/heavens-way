using HeavensWayApi.Repositories;
using HeavensWayApi.Dto;
using HeavensWayApi.Entities;
using System.Text.Json;
using HeavensWayApi.Services.Interfaces;
using HeavensWayApi.Repositories.Interfaces;

namespace HeavensWayApi.Services
{
    public class EnderecoService : IEnderecoService
    {
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly HttpClient _httpClient;

        const string URI = "https://opencep.com/v1/";

        public EnderecoService(IEnderecoRepository enderecoRepository)
        {
            _enderecoRepository = enderecoRepository;
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(URI)
            };
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
                    var responseBody = await response.Content.ReadFromJsonAsync<ResponseEnderecoDto>();
                    endereco = new Endereco(responseBody);
                    _enderecoRepository.Create(endereco);
                }
            }
            catch(HttpRequestException ex)
            {
                throw new Exception($"Falha ao recuperar informações de endereço para o CEP: {cep}. {ex.Message}");
            }

            return endereco;
        }
    }
}