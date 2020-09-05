using System;
using System.Collections.Generic;
using System.Text;

namespace POS.BAL.Models
{
    public class POSMainModel
    {
        public string Id { get; set; }
        public DateTime PosDate { get; set; }
        public string CustomerId { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
