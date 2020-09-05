using System;
using System.Collections.Generic;
using System.Text;

namespace POS.DAL.Models
{
    public class POSDetail
    {
        public string Id { get; set; }
        public string POSMainID { get; set; }
        public string ItemId { get; set; }
        public string SaleOrReturn { get; set; }
        public int ItemQuantity { get; set; }
        public decimal ItemRate { get; set; }
        public decimal TotalAmount { get; set; }

        public POSMain POSMain { get; set; }
        public MasterItem MasterItem { get; set; }

    }
}
