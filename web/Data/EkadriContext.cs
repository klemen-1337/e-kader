using web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace web.Data
{
    public class EkadriContext : IdentityDbContext<Uporabniki>
    {
        public EkadriContext(DbContextOptions<EkadriContext> options) : base(options)
        {
        }

        public DbSet<DelovnaMesta> DelovnaMesta { get; set; }
        public DbSet<DelovneUre> DelovneUre { get; set; }
        public DbSet<Dopust> Dopusti { get; set; }
        public DbSet<Izobrazevanje> Izobrazevanja { get; set; }
        public DbSet<Zaposlen> Zaposleni { get; set; }

    }
}