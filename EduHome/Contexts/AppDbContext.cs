using EduHome.Identity;
using EduHome.Models;
using EduHome.Models.common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Contexts
{
    public class AppDbContext :IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { 
        }

        public DbSet<Slider> Sliders { get; set; } = null!;
		public DbSet<Events> Events { get; set; } = null!;
		public DbSet<Speakers> Speakers { get; set; } = null!;
		public DbSet<SpeakersEvent> SpeakersEvents { get; set; } = null!;

      public override  Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<BaseSectionEntity>();
            foreach (var entry in entries)
            {
              
               switch (entry.State)
                {
                    case EntityState.Added:
						entry.Entity.CreatedDate = DateTime.UtcNow;
						entry.Entity.CreatedBy = "Admin";
						entry.Entity.UpdatedDate = DateTime.UtcNow;
						entry.Entity.UpdatedBy = "Admin";
                        break;
                        case EntityState.Modified:
						entry.Entity.UpdatedDate = DateTime.UtcNow;
						entry.Entity.UpdatedBy = "Admin";
                        break;
				}
            }
            return base.SaveChangesAsync(cancellationToken);
              
        }

    }
}
