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
            builder.Property(c => c.Name).HasMaxLength(500);
            builder.Property(c => c.Lot).HasMaxLength(500);
            builder.Property(c => c.UnitId).HasMaxLength(100);

            builder
                .HasOne(m => m.Yarn);
        }
    }
}
