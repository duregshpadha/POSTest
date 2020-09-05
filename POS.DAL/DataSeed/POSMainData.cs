using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.DAL.DataSeed
{
    class POSMainData
    {
        internal static EntityTypeBuilder<POSMain> Seed(EntityTypeBuilder<POSMain> builder)
        {
            builder.HasData(new POSMain[]
            {
                new POSMain()
                {
                    Id="09052020-074037271-6be8d386-4ad5-4e92-b827",
                    CustomerId="09052020-074005322-f7390afc-c276-415f-9a20",
                    PosDate=DateTime.Now,
                    TotalAmount=10,
                    TotalQuantity=1
                }
            });

            return builder;
        }
    }
}
