using HeavensWayApi.Entities;

namespace HeavensWayApi.Repositories.Interfaces
{
    public interface ITipoEventoRepository
    {
        public TipoEvento GetById(int id);
        public IEnumerable<TipoEvento> GetByDescription(string description);
        public IEnumerable<TipoEvento> GetAll();
        public void Create(TipoEvento tipoEvento);
        public void Update(TipoEvento tipoEvento);
        public void Delete(TipoEvento tipoEvento);
    }
}