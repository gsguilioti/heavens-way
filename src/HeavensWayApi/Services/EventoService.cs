using HeavensWayApi.Repositories;
using HeavensWayApi.Repositories.Interfaces;
using HeavensWayApi.Services.Interfaces;

namespace HeavensWayApi.Services
{
    public class EventoService : IEventoService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IEventoRepository _eventoRepository;

        public EventoService(IUsuarioRepository usuarioRepository, IEventoRepository eventoRepository)
        {
            _usuarioRepository = usuarioRepository;
            _eventoRepository = eventoRepository;
        }

        public bool Inscrever(int idEvento, int idUsuario)
        { 
            var evento = _eventoRepository.GetById(idEvento);
            if(evento == null)
                return false;

            var usuario = _usuarioRepository.GetById(idUsuario);
            if(usuario == null)
                return false;

            _usuarioRepository.Inscrever(evento, usuario);
            return true;
        }
    }
}