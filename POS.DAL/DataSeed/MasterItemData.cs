using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.DAL.Models;

namespace POS.DAL.DataSeed
{
    class MasterItemData
    {
        internal static EntityTypeBuilder<MasterItem> Seed(EntityTypeBuilder<MasterItem> builder)
        {
            builder.HasData(new MasterItem[]
            {
                new MasterItem()
                {
                    Id="09052020-073914277-53924f75-cae5-475b-a5eb",
                    Name="Rice",
                    Rate=10,
                    Stock=10
                }
            });

            return builder;
        }
    }
}
