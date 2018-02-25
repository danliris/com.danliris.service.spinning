using Com.Danliris.Service.Spinning.Lib.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Spinning.Lib.Configs
{
    class SpinningInputProductionConfig : IEntityTypeConfiguration<SpinningInputProduction>
    {

        public void Configure(EntityTypeBuilder<SpinningInputProduction> builder)
        {
            builder.Property(c => c.NomorInputProduksi).HasMaxLength(100);

            builder.Property(c => c.NomorInputProduksi).HasMaxLength(100);
            builder.Property(c => c.UnitId).HasMaxLength(500);
            builder.Property(c => c.UnitName).HasMaxLength(500);
            builder.Property(c => c.MachineId).HasMaxLength(500);
            builder.Property(c => c.MachineName).HasMaxLength(500);
            builder.Property(c => c.Shift).HasMaxLength(500);
            builder.Property(c => c.YarnId).HasMaxLength(100);
            builder.Property(c => c.YarnName).HasMaxLength(500);

        }
    }
}
