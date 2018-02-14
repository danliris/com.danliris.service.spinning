using Com.Danliris.Service.Spinning.Lib.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Com.Danliris.Service.Spinning.Lib.Configs
{
    class LotYarnConfig : IEntityTypeConfiguration<LotYarn>
    {
        public void Configure(EntityTypeBuilder<LotYarn> builder)
        {
            builder.Property(c => c.Code).HasMaxLength(100);
            builder.Property(c => c.Lot).HasMaxLength(500);
            builder.Property(c => c.UnitId).HasMaxLength(100);
            builder.Property(c => c.UnitCode).HasMaxLength(100);
            builder.Property(c => c.UnitName).HasMaxLength(100);
            builder.Property(c => c.YarnCode).HasMaxLength(100);
            builder.Property(c => c.YarnName).HasMaxLength(100);
            builder.Property(c => c.MachineId).HasMaxLength(100);
            builder.Property(c => c.MachineCode).HasMaxLength(100);
            builder.Property(c => c.MachineName).HasMaxLength(100);
        }
    }
}
