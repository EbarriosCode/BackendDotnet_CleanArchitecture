using Countries.Infra.Data.DataContext;
using Microsoft.EntityFrameworkCore;
using System;

namespace Countries.UnitTests.ResourcesDatabase
{
    public class BaseContextTest : IDisposable
    {
        protected readonly CountriesDbContext _context;

        public BaseContextTest()
        {
            var options = new DbContextOptionsBuilder<CountriesDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            this._context = new CountriesDbContext(options);
            this._context.Database.EnsureCreated();

            DbInitializerTest.Initialize(this._context);
        }

        public void Dispose()
        {
            this._context.Database.EnsureDeleted();
            this._context.Dispose();
        }
    }
}
