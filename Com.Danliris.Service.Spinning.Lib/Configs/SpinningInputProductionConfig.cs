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

            builder
            .HasMany(m => m.Input);


        }
    }
}
