using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Template.Infrastructure.Data.Mappings;
using Template.Infrastructure.Data.Mappings.Extensions;
using Template.Domain.Aggregates.Clientes;

namespace Template.Infrastructure.Data.Context
{
    public class DataContext : DbContext
    {
        public static ILoggerFactory _loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

        #region DbSets

        public DbSet<Cliente> Cliente { get; set; }

        #endregion

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.Ignore<ValidationFailure>();

            modelBuilder.AddConfiguration(new ClienteMap());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseLoggerFactory(_loggerFactory)
                .EnableSensitiveDataLogging();
        }
    }
}
