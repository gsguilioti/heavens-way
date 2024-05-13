using HeavensWayApi.Data;
using HeavensWayApi.Entities;
using HeavensWayApi.Repositories.Interfaces;

namespace HeavensWayApi.Repositories
{
    public class DistritoRepository : IDistritoRepository
    {
        private readonly DataContext _context;
        public DistritoRepository(DataContext context)
        {
            _context = context;
        }

        public Distrito GetById(int id) => _context.Distritos.FirstOrDefault(t => t.Id == id);

        public IEnumerable<Distrito> GetAll() => _context.Distritos;

        public void Create(Distrito distrito)
        {
            _context.Distritos.Add(distrito);
            _context.SaveChanges();
        }

        public void Update(Distrito distrito)
        {
            _context.Distritos.Update(distrito);
            _context.SaveChanges();
        }

        public void Delete(Distrito distrito)
        {
            _context.Distritos.Remove(distrito);
            _context.SaveChanges();
        }
    }
}
