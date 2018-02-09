using Com.Danliris.Service.Spinning.Lib;
using Com.Danliris.Service.Spinning.Lib.Services;
using Com.Danliris.Service.Spinning.Test.DataUtils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace Com.Danliris.Service.Spinning.Test
{
    public class ServiceProviderFixture : IDisposable
    {
        public IServiceProvider ServiceProvider { get; private set; }
        public ServiceProviderFixture()
        {
            /* For unit test by local machine (using localdb) */
            //string connectionString = "Server=(localdb)\\mssqllocaldb;Database=com.danliris.db.spinning.test;Trusted_Connection=True;";

            /* For unit test by Travis CI (using docker container mssql) */
            string connectionString = "Server=localhost,1401;Database=com.danliris.db.spinning.test;User=sa;password=Standar123;MultipleActiveResultSets=true;";

            this.ServiceProvider = new ServiceCollection()

                .AddDbContext<SpinningDbContext>((serviceProvider, options) =>
                {
                    options.UseSqlServer(connectionString);
                }, ServiceLifetime.Transient)
                .AddTransient<YarnService>(provider => new YarnService(provider))
                .AddTransient<YarnOutputProductionService>(provider => new YarnOutputProductionService(provider))
                .AddTransient<YarnServiceDataUtil>()
                .AddTransient<YarnOutputProductionServiceDataUtil>()
                .BuildServiceProvider();

            SpinningDbContext dbContext = ServiceProvider.GetService<SpinningDbContext>();
            dbContext.Database.Migrate();
        }

        public void Dispose()
        {
        }
    }

    [CollectionDefinition("ServiceProviderFixture collection")]
    public class ServiceProviderFixtureCollection : ICollectionFixture<ServiceProviderFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
