using System;
using System.Collections.Generic;
using System.Text;

namespace POS.DAL.Models
{
    public class POSMain
    {
        public string Id { get; set; }
        public DateTime PosDate { get; set; }
        public string CustomerId { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalAmount { get; set; }

        public MasterCustomer MasterCustomer { get; set; }

        public virtual ICollection<POSDetail> POSDetails { get; set; }
    }
}
