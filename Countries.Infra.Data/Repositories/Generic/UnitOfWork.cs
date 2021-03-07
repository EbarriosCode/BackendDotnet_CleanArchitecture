using Countries.Infra.Data.DataContext;
using System;

namespace Countries.Infra.Data.Repositories.Generic
{
    public interface IUnitOfWork : IDisposable
    {
        CountriesDbContext Context { get; }
        void Commit();
    }
    public class UnitOfWork : IUnitOfWork
    {
        public CountriesDbContext Context { get; }

        public UnitOfWork(CountriesDbContext context)
        {
            Context = context;
        }

        public void Commit()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
