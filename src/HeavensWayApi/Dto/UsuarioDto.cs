using HeavensWayApi.Entities;

namespace HeavensWayApi.Dto
{
    public class UsuarioDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Nome { get; set; }
        public int IgrejaId { get; set; }
        public string Role { get; set; }
        public UsuarioDto() { }

        public UsuarioDto(Usuario usuario)
        {
            UserName = usuario.UserName;
            Email = usuario.Email;
            Nome = usuario.Nome;
            IgrejaId = usuario.IgrejaId;
        }
    }
}
