using Microsoft.EntityFrameworkCore;
using Piccadilly_Roma_Be.Models;

namespace Piccadilly_Roma_Be.Data
{
    public class RistoranteContext : DbContext
    {
        public RistoranteContext(DbContextOptions<RistoranteContext> options) : base(options) { }
        
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Evento> Eventi { get; set; }
        public DbSet<Prenotazione> Prenotazione { get; set; }

}
}