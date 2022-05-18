using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Template.Shared.Kernel.Data;
using System.Threading.Tasks;

namespace Template.Api.Data.Context
{
    public class DataContext : DbContext, IUnitOfWork
    {
        public static ILoggerFactory _loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

        #region DbSets



        #endregion

        public DataContext(DbContextOptions<DataContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.Ignore<ValidationFailure>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseLoggerFactory(_loggerFactory)
                .EnableSensitiveDataLogging();
        }

        public async Task<bool> CommitAsync()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}