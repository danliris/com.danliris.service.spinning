using Com.Danliris.Service.Spinning.Lib.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Com.Danliris.Service.Spinning.Lib.Configs
{
    public class YarnOutputProductionConfig : IEntityTypeConfiguration<YarnOutputProduction>
    {
        public void Configure(EntityTypeBuilder<YarnOutputProduction> builder)
        {
            builder.Property(c => c.Code).HasMaxLength(100);
            builder.Property(c => c.Spinning).HasMaxLength(500);
            builder.Property(c => c.LotYarnCode).HasMaxLength(10);
            builder.Property(c => c.LotYarnId).HasMaxLength(500);
            builder.Property(c => c.LotYarnName).HasMaxLength(500);
            builder.Property(c => c.MachineId).HasMaxLength(500);
            builder.Property(c => c.MachineName).HasMaxLength(500);
            builder.Property(c => c.Shift).HasMaxLength(500);
            builder.Property(c => c.YarnCode).HasMaxLength(100);
            builder.Property(c => c.YarnId).HasMaxLength(500);
            builder.Property(c => c.YarnName).HasMaxLength(500);
        }
    }
}