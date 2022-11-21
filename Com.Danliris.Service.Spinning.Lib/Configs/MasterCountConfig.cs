using Com.Danliris.Service.Spinning.Lib.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Spinning.Lib.Configs
{
    public class MasterCountConfig : IEntityTypeConfiguration<MasterCount>
    {
        public void Configure(EntityTypeBuilder<MasterCount> builder)
        {
            builder.Property(c => c.Count).HasMaxLength(200);
            builder.Property(c => c.Remark).HasMaxLength(200);
        }
    }
}
