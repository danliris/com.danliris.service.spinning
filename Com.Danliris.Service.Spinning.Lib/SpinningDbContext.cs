using Com.Moonlay.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Com.Danliris.Service.Spinning.Lib.Models;
using Com.Danliris.Service.Spinning.Lib.Configs;

namespace Com.Danliris.Service.Spinning.Lib
{
    public class SpinningDbContext : BaseDbContext
    {
        public SpinningDbContext(DbContextOptions<SpinningDbContext> options) : base(options)
        {
        }

        public DbSet<Yarn> Yarns { get; set; }
        public DbSet<LotYarn> LotYarns { get; set; }
        public DbSet<SpinningInputProduction> WinderInputProductions { get; set; }
        public DbSet<WinderOutputProduction> WinderOutputProductions { get; set; }
        public DbSet<MasterCount> MasterCounts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new YarnConfig());
            modelBuilder.ApplyConfiguration(new LotYarnConfig());
            modelBuilder.ApplyConfiguration(new SpinningInputProductionConfig());
            modelBuilder.ApplyConfiguration(new WinderOutputProductionConfig());
            modelBuilder.ApplyConfiguration(new MasterCountConfig());

        }
    }
}