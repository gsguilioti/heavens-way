using HeavensWayApi.Data;
using HeavensWayApi.Entities;

namespace HeavensWayApi.Repositories
{
    public class TipoEventoRepository
    {
        private readonly DataContext _context;
        public TipoEventoRepository(DataContext context)
        {
            _context = context;
        }

        public TipoEvento GetById(int id) => _context.TiposEvento.FirstOrDefault(t => t.Id == id);

        public IEnumerable<TipoEvento> GetAll() => _context.TiposEvento;

        public void Create(TipoEvento tipoEvento)
        {
            _context.TiposEvento.Add(tipoEvento);
            _context.SaveChanges();
        }

        public void Update(TipoEvento tipoEvento)
        {
            _context.TiposEvento.Update(tipoEvento);
            _context.SaveChanges();
        }

        public void Delete(TipoEvento tipoEvento)
        {
            _context.TiposEvento.Remove(tipoEvento);
            _context.SaveChanges();
        }
    }
}