using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Constants.DALConstants;
using POS.DAL.DataSeed;
using POS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.DAL.ModelConfiguration
{
    class POSDetailConfiguration : IEntityTypeConfiguration<POSDetail>
    {
        public void Configure(EntityTypeBuilder<POSDetail> builder)
        {
            builder.ToTable(nameof(POSDetail), "dbo");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasMaxLength(MaxLengthConstant.ID).IsRequired();
            builder.Property(e => e.POSMainID).HasMaxLength(MaxLengthConstant.ID).IsRequired();
            builder.Property(e => e.ItemId).HasMaxLength(MaxLengthConstant.ID).IsRequired();
            builder.Property(e => e.SaleOrReturn).HasMaxLength(MaxLengthConstant.One).IsRequired();
            builder.Property(e => e.ItemQuantity).IsRequired();
            builder.Property(e => e.ItemRate).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(e => e.TotalAmount).HasColumnType("decimal(18,2)").IsRequired();

            builder.HasOne(e => e.POSMain).WithMany(e => e.POSDetails).HasForeignKey(e => e.POSMainID)
                .OnDelete(DeleteBehavior.Cascade).IsRequired();

            builder.HasOne(e => e.MasterItem).WithMany(e => e.POSDetails).HasForeignKey(e => e.ItemId)
                .OnDelete(DeleteBehavior.Cascade).IsRequired();

            POSDetailData.Seed(builder);
        }
    }
}
