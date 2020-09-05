using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.DAL.DataSeed
{
    class POSDetailData
    {
        internal static EntityTypeBuilder<POSDetail> Seed(EntityTypeBuilder<POSDetail> builder)
        {
            builder.HasData(new POSDetail[]
            {
                new POSDetail()
                {
                    Id="09052020-073953202-81be9799-750d-4955-bb17",
                    POSMainID="09052020-074037271-6be8d386-4ad5-4e92-b827",
                    ItemId="09052020-073914277-53924f75-cae5-475b-a5eb",
                    SaleOrReturn="S",
                    ItemQuantity=1,
                    ItemRate=10,
                    TotalAmount=10
                }
            });
            return builder;
        }
    }
}
