using HeavensWayApi.Repositories;

namespace HeavensWayApi.Services
{
    public class EventoService
    {
        private readonly UsuarioRepository _usuarioRepository;
        private readonly EventoRepository _eventoRepository;

        public EventoService(UsuarioRepository usuarioRepository, EventoRepository eventoRepository)
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