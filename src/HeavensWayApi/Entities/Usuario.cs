using Microsoft.AspNetCore.Identity;

namespace HeavensWayApi.Entities
{
    public class Usuario : IdentityUser
    {
        public string Nome {get; set; }

        public int IgrejaId { get; set; }
        public Igreja Igreja { get; set; }

        ICollection<Evento> Eventos { get; set; }
    }
}
