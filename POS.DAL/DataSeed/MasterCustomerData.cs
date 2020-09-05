using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.DAL.Models;

namespace POS.DAL.DataSeed
{
    class MasterCustomerData
    {
        internal static EntityTypeBuilder<MasterCustomer> Seed(EntityTypeBuilder<MasterCustomer> builder)
        {
            builder.HasData(new MasterCustomer[]
            {
                new MasterCustomer()
                {
                    Id="09052020-074005322-f7390afc-c276-415f-9a20",
                    Name="Ram",
                    Phone="1234567890"
                }
            });
            return builder;
        }
    }
}
