using HeavensWayApi.Entities;

namespace HeavensWayApi.Repositories.Interfaces
{
    public interface IDistritoRepository
    {
        Distrito GetById(int id);
        IEnumerable<Distrito> GetAll();
        void Create(Distrito distrito);
        void Update(Distrito distrito);
        void Delete(Distrito distrito);
    }
}