using HeavensWayApi.Dto;

using Microsoft.AspNetCore.Identity;

namespace HeavensWayApi.Entities
{
    public class Usuario : IdentityUser<int>
    {
        public string Nome {get; set; }

        public int IgrejaId { get; set; }
        public Igreja Igreja { get; set; }

        public ICollection<Evento> Eventos { get; set; }

        public Usuario() {}
        public Usuario(UsuarioDto dto)
        {
            Nome = dto.Nome;
            IgrejaId = dto.IgrejaId;
        }

        public void MapDto(UsuarioDto dto)
        {
            Nome = dto.Nome;
            IgrejaId = dto.IgrejaId;
        }
    }
}
