using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChauffeurApiCORE.Models
{
	public class ApplicationUser : IdentityUser
    {
        
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IChaufferDbContext
    {
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}

        public DbSet<Driver> Dirvers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Stop> Stops { get; set; }

        public void SetDeleted(object entity)
        {
            Entry(entity).State = EntityState.Deleted;
        }

        public void SetUpdated(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }

		public override int SaveChanges()
		{
			try
			{
				return base.SaveChanges();
			}
			catch (DbUpdateException ex)
			{
				throw new DbUpdateException(ex.InnerException?.InnerException?.Message?.ToString() ?? ex.Message, ex.InnerException);
			}
		}

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			return base.SaveChangesAsync(cancellationToken);
		}
	}


    public interface IChaufferDbContext
    {
        DbSet<Driver> Dirvers { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<Booking> Bookings { get; set; }
        DbSet<Stop> Stops { get; set; }

        int SaveChanges();
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
		void SetDeleted(object entity);
        void SetUpdated(object entity);
    }
}