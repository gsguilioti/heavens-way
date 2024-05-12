using HeavensWayApi.Entities;

namespace HeavensWayApi.Dto
{
    public class GetUsuarioDto
    {
        public string UserName { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public int IgrejaId { get; set; }
        public GetUsuarioDto() { }

        public GetUsuarioDto(Usuario usuario)
        {
            UserName = usuario.UserName;
            Email = usuario.Email;
            Nome = usuario.Nome;
            IgrejaId = usuario.IgrejaId;
        }
    }
}
