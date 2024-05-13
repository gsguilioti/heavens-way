using HeavensWayApi.Entities;

namespace HeavensWayApi.Services.Interfaces
{
    public interface IEnderecoService
    {
        Task<Endereco> Create(string cep);
    }
}
