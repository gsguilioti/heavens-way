using HeavensWayApi.Entities;

namespace HeavensWayApi.Dto
{
    public class LoginUsuarioDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public LoginUsuarioDto() { }
        public LoginUsuarioDto(Usuario usuario)
        {
            UserName = usuario.UserName;
        }
    }
}
