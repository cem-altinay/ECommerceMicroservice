using Microsoft.EntityFrameworkCore;
using NotificationService.Domain.Entities;
using NotificationService.Infrastructure.EntityConfigurations;

namespace NotificationService.Infrastructure.Data
{
	public class NotificationContext : DbContext
	{
        public NotificationContext()
        {
            
        }
        public NotificationContext(DbContextOptions<NotificationContext> options) : base(options)
		{
		}

		public DbSet<Notification> Notifications { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new NotificationEntityConfiguration());
		}
	}

}
