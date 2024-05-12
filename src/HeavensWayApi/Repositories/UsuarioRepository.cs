using HeavensWayApi.Data;
using HeavensWayApi.Entities;

namespace HeavensWayApi.Repositories
{
    public class UsuarioRepository
    {
        private readonly DataContext _context;
        public UsuarioRepository(DataContext context)
        {
            _context = context;
        }

        public Usuario GetById(int id) => _context.Usuarios.FirstOrDefault(t => t.Id == id);

        public IEnumerable<Usuario> GetAll() => _context.Usuarios;

        public void Create(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public void Update(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            _context.SaveChanges();
        }

        public void Delete(Usuario usuario)
        {
            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
        }
    }
}