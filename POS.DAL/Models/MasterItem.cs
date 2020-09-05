using System;
using System.Collections.Generic;
using System.Text;

namespace POS.DAL.Models
{
    public class MasterItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Rate { get; set; }
        public int Stock { get; set; }

        public virtual ICollection<POSDetail> POSDetails { get; set; }
    }
}
