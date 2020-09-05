using System;
using System.Collections.Generic;
using System.Text;

namespace POS.BAL.Models
{
    public class MasterItemModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Rate { get; set; }
        public int Stock { get; set; }
    }
}
