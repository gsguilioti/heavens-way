using HeavensWayApi.Data;
using HeavensWayApi.Entities;
using HeavensWayApi.Repositories.Interfaces;

namespace HeavensWayApi.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DataContext _context;
        public UsuarioRepository(DataContext context)
        {
            _context = context;
        }

        public Usuario GetById(int id) => _context.Usuarios.FirstOrDefault(u => u.Id == id);
        public IEnumerable<Usuario> GetByIgreja(int id) => _context.Usuarios.Where(u => u.IgrejaId == id);
        public IEnumerable<Usuario> GetByEvento(int id) => _context.Usuarios.Where(u => u.Eventos.Any(e => e.Id == id));
        public void Inscrever(Evento evento, Usuario usuario)
        {
            usuario.Eventos.Add(evento);
            _context.Usuarios.Update(usuario);
            _context.SaveChanges();
        }
    }
}