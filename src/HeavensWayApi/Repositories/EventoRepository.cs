using HeavensWayApi.Data;
using HeavensWayApi.Entities;
using HeavensWayApi.Repositories.Interfaces;

namespace HeavensWayApi.Repositories
{
    public class EventoRepository : IEventoRepository
    {
        private readonly DataContext _context;
        public EventoRepository(DataContext context)
        {
            _context = context;
        }

        public Evento GetById(int id) => _context.Eventos.FirstOrDefault(t => t.Id == id);
        public IEnumerable<Evento> GetByUsuario(int id) => _context.Eventos.Where(e => e.Usuarios.Any(u => u.Id == id));
        public IEnumerable<Evento> GetByIgreja(int id) => _context.Eventos.Where(e => e.Igrejas.Any(i => i.Id == id));
        public IEnumerable<Usuario> GetInscritos(int id)
        {
            return _context.Eventos.Where(e => e.Id == id)
                                           .SelectMany(e => e.Usuarios)
                                           .ToList();
        }

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

        public void AddIgreja(Igreja igreja, int eventoId)
        {
            var evento = _context.Eventos.FirstOrDefault(e => e.Id == eventoId);
            evento.Igrejas = new List<Igreja>();
            evento.Igrejas.Add(igreja);
            _context.SaveChanges();
        }
    }
}