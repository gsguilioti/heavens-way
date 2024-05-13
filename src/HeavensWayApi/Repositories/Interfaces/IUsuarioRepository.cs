using HeavensWayApi.Entities;

namespace HeavensWayApi.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        Usuario GetById(int id);
        IEnumerable<Usuario> GetByIgreja(int id);
        IEnumerable<Usuario> GetByEvento(int id);
        void Inscrever(Evento evento, Usuario usuario);
    }
}