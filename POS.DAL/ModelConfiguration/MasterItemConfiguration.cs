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
    class MasterItemConfiguration : IEntityTypeConfiguration<MasterItem>
    {
        public void Configure(EntityTypeBuilder<MasterItem> builder)
        {
            builder.ToTable(nameof(MasterItem), "dbo");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasMaxLength(MaxLengthConstant.ID).IsRequired();
            builder.Property(e => e.Name).HasMaxLength(MaxLengthConstant.ShortStringLenght).IsRequired();
            builder.Property(e => e.Rate).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(e => e.Stock).IsRequired();

            MasterItemData.Seed(builder);
        }
    }
}
