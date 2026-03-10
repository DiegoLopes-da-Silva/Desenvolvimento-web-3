using Microsoft.EntityFrameworkCore;

namespace VasosInteligentes.Data
{
    public class FakeContext:DbContext
    {
        public DbSet<Vasos> Vasos { get; set; }
        public DbSet<Plantas> Plantas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("Banco Temporário");
        }
    }
}
