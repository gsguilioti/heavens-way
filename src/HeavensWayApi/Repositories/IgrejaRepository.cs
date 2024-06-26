using HeavensWayApi.Data;
using HeavensWayApi.Entities;
using HeavensWayApi.Repositories.Interfaces;

namespace HeavensWayApi.Repositories
{
    public class IgrejaRepository : IIgrejaRepository
    {
        private readonly DataContext _context;
        public IgrejaRepository(DataContext context)
        {
            _context = context;
        }

        public Igreja GetById(int id) => _context.Igrejas.FirstOrDefault(t => t.Id == id);
        public IEnumerable<Igreja> GetByDistrito(int id) => _context.Igrejas.Where(i => i.DistritoId == id);
        public IEnumerable<Igreja> GetAll() => _context.Igrejas;

        public void Create(Igreja igreja)
        {
            _context.Igrejas.Add(igreja);
            _context.SaveChanges();
        }

        public void Update(Igreja igreja)
        {
            _context.Igrejas.Update(igreja);
            _context.SaveChanges();
        }

        public void Delete(Igreja igreja)
        {
            _context.Igrejas.Remove(igreja);
            _context.SaveChanges();
        }
    }
}