using HeavensWayApi.Entities;

namespace HeavensWayApi.Repositories.Interfaces
{
    public interface IEventoRepository
    {
        public Evento GetById(int id);
        public IEnumerable<Evento> GetByUsuario(int id);
        public IEnumerable<Evento> GetByIgreja(int id);
        public IEnumerable<Usuario> GetInscritos(int id);
        public IEnumerable<Evento> GetAll();
        public void Create(Evento evento);
        public void Update(Evento evento);
        public void Delete(Evento evento);
    }
}