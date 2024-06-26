using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HeavensWayApi.Entities;

namespace HeavensWayApi.Data
{
    public class DataContext : IdentityDbContext<Usuario, IdentityRole<int>, int>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Distrito> Distritos { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Igreja> Igrejas { get; set; }
        public DbSet<TipoEvento> TiposEvento { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}