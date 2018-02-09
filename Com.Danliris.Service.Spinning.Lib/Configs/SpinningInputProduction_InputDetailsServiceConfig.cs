using Com.Danliris.Service.Spinning.Lib.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Com.Danliris.Service.Spinning.Lib.Configs
{
    class SpinningInputProduction_InputDetailsServiceConfig : IEntityTypeConfiguration<SpinningInputProduction_InputDetails>
    {


        public void Configure(EntityTypeBuilder<SpinningInputProduction_InputDetails> builder)
        {
            builder.Property(c => c.Id).HasMaxLength(100);

        }
    }
}
