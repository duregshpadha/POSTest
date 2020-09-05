using System;
using System.Collections.Generic;
using System.Text;

namespace POS.BAL.Models
{
    public class POSDetailModel
    {
        public string Id { get; set; }
        public string POSMainID { get; set; }
        public string ItemId { get; set; }
        public string SaleOrReturn { get; set; }
        public int ItemQuantity { get; set; }
        public decimal ItemRate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
