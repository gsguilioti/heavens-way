using Microsoft.EntityFrameworkCore;
using HeavensWayApi.Entities;

namespace HeavensWayApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}