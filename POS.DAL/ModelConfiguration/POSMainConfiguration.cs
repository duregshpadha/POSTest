using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Constants.DALConstants;
using POS.DAL.DataSeed;
using POS.DAL.Models;
using System;

namespace POS.DAL.ModelConfiguration
{
    class POSMainConfiguration : IEntityTypeConfiguration<POSMain>
    {
        public void Configure(EntityTypeBuilder<POSMain> builder)
        {
            builder.ToTable(nameof(POSMain), "dbo");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasMaxLength(MaxLengthConstant.ID).IsRequired();
            builder.Property(e => e.PosDate).HasColumnType("datetime2").IsRequired();
            builder.Property(e => e.CustomerId).HasMaxLength(MaxLengthConstant.ID).IsRequired();
            builder.Property(e => e.TotalQuantity).IsRequired();
            builder.Property(e => e.TotalAmount).HasColumnType("decimal(18,2)").IsRequired();

            builder.HasOne(e => e.MasterCustomer).WithMany(e => e.POSMains).HasForeignKey(e => e.CustomerId)
                .OnDelete(DeleteBehavior.Cascade).IsRequired();

            POSMainData.Seed(builder);
        }
    }
}
