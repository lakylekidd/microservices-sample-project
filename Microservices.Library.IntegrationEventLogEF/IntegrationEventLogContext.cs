using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservices.Library.IntegrationEventLogEF
{
    /// <summary>
    /// The integration event log context
    /// </summary>
    public class IntegrationEventLogContext : DbContext
    {
        /// <summary>
        /// The integration event logs
        /// </summary>
        public DbSet<IntegrationEventLogEntry> IntegrationEventLogs { get; set; }

        // The constructor
        public IntegrationEventLogContext(DbContextOptions<IntegrationEventLogContext> options) 
            : base(options)
        { }

        // On model creating method
        protected override void OnModelCreating(ModelBuilder builder)
        {   
            // Configure the event log entry
            builder.Entity<IntegrationEventLogEntry>(ConfigureIntegrationEventLogEntry);
        }

        // Configure the integration event log entry
        static void ConfigureIntegrationEventLogEntry(EntityTypeBuilder<IntegrationEventLogEntry> builder)
        {
            builder.ToTable("IntegrationEventLog");

            builder.HasKey(e => e.EventId);

            builder.Property(e => e.EventId)
                .IsRequired();

            builder.Property(e => e.Content)
                .IsRequired();

            builder.Property(e => e.CreationTime)
                .IsRequired();

            builder.Property(e => e.State)
                .IsRequired();

            builder.Property(e => e.TimesSent)
                .IsRequired();

            builder.Property(e => e.EventTypeName)
                .IsRequired();

        }
    }

    public class OrderingContextDesignFactory : IDesignTimeDbContextFactory<IntegrationEventLogContext>
    {
        public IntegrationEventLogContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<IntegrationEventLogContext>()
                .UseSqlServer("Server=.;Initial Catalog=Microservices.IntegrationEventLogDB;Integrated Security=true");

            return new IntegrationEventLogContext(optionsBuilder.Options);
        }
    }
}
