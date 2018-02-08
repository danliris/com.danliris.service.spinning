using Com.Danliris.Service.Spinning.Lib.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Com.Danliris.Service.Spinning.Lib.Configs
{
    public class YarnConfig : IEntityTypeConfiguration<Yarn>
    {
        public void Configure(EntityTypeBuilder<Yarn> builder)
        {
            builder.Property(c => c.Code).HasMaxLength(100);
            builder.Property(c => c.Name).HasMaxLength(500);
            builder.Property(c => c.Remark).HasMaxLength(500);
        }
    }
}
