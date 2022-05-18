using Microsoft.EntityFrameworkCore;
using Template.Infrastructure.Data.Context;
using System;

namespace Template.UnitTests.Fixtures
{
    public class DataContextFixture
    {
        public DataContext ObterDataContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new DataContext(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            return context;
        }
    }
}
