using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace MedicalSystem.Infrastructure
{
	public class MainContext : DbContext
	{
        public MainContext(DbContextOptions<MainContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<MedCard> MedCards { get; set; }
	}
}
