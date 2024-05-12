using HeavensWayApi.Data;
using HeavensWayApi.Entities;

namespace HeavensWayApi.Repositories
{
    public class EnderecoRepository
    {
        private readonly DataContext _context;
        public EnderecoRepository(DataContext context)
        {
            _context = context;
        }

        public Endereco GetById(int id) => _context.Enderecos.FirstOrDefault(t => t.Id == id);

        public IEnumerable<Endereco> GetAll() => _context.Enderecos;

        public void Create(Endereco endereco)
        {
            _context.Enderecos.Add(endereco);
            _context.SaveChanges();
        }

        public void Update(Endereco endereco)
        {
            _context.Enderecos.Update(endereco);
            _context.SaveChanges();
        }

        public void Delete(Endereco endereco)
        {
            _context.Enderecos.Remove(endereco);
            _context.SaveChanges();
        }
    }
}