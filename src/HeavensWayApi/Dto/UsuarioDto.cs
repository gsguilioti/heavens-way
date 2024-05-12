using HeavensWayApi.Entities;

namespace HeavensWayApi.Dto
{
    public class UsuarioDto
    {
        public string Nome { get; set; }
        public int IgrejaId { get; set; }

        public UsuarioDto(Usuario usuario)
        {
            Nome = usuario.Nome;
            IgrejaId = usuario.IgrejaId;
        }
    }
}
