using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Constants.DALConstants;
using POS.DAL.DataSeed;
using POS.DAL.Models;


namespace POS.DAL.ModelConfiguration
{
    class MasterCustomerConfiguration : IEntityTypeConfiguration<MasterCustomer>
    {
        public void Configure(EntityTypeBuilder<MasterCustomer> builder)
        {
            builder.ToTable(nameof(MasterCustomer), "dbo");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasMaxLength(MaxLengthConstant.ID).IsRequired();
            builder.Property(e => e.Name).HasMaxLength(MaxLengthConstant.ShortStringLenght).IsRequired();
            builder.Property(e => e.Phone).HasMaxLength(MaxLengthConstant.Phone).IsRequired();

            builder.HasIndex(e => e.Phone).IsUnique();

            MasterCustomerData.Seed(builder);
        }
    }
}
