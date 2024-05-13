using HeavensWayApi.Entities;

namespace HeavensWayApi.Repositories.Interfaces
{
    public interface IIgrejaRepository
    {
        public Igreja GetById(int id);
        public IEnumerable<Igreja> GetByDistrito(int id);
        public IEnumerable<Igreja> GetAll();
        public void Create(Igreja igreja);
        public void Update(Igreja igreja);
        public void Delete(Igreja igreja);
    }
}