using HeavensWayApi.Data;
using HeavensWayApi.Entities;
using HeavensWayApi.Repositories.Interfaces;

namespace HeavensWayApi.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly DataContext _context;
        public EnderecoRepository(DataContext context)
        {
            _context = context;
        }

        public Endereco GetById(int id) => _context.Enderecos.FirstOrDefault(t => t.Id == id);
        public Endereco GetByCep(string cep) => _context.Enderecos.FirstOrDefault(t => t.Cep.Equals(cep));
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