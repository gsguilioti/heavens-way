using HeavensWayApi.Entities;

namespace HeavensWayApi.Repositories.Interfaces
{
    public interface IEnderecoRepository
    {
        Endereco GetById(int id);
        Endereco GetByCep(string cep);
        IEnumerable<Endereco> GetAll();
        void Create(Endereco endereco);
        void Update(Endereco endereco);
        void Delete(Endereco endereco);
    }
}