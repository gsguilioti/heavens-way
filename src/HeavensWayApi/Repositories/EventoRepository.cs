using HeavensWayApi.Data;
using HeavensWayApi.Entities;

namespace HeavensWayApi.Repositories
{
    public class EventoRepository
    {
        private readonly DataContext _context;
        public EventoRepository(DataContext context)
        {
            _context = context;
        }

        public Evento GetById(int id) => _context.Eventos.FirstOrDefault(t => t.Id == id);

        public IEnumerable<Evento> GetAll() => _context.Eventos;

        public void Create(Evento evento)
        {
            _context.Eventos.Add(evento);
            _context.SaveChanges();
        }

        public void Update(Evento evento)
        {
            _context.Eventos.Update(evento);
            _context.SaveChanges();
        }

        public void Delete(Evento evento)
        {
            _context.Eventos.Remove(evento);
            _context.SaveChanges();
        }
    }
}