using System;
using System.Collections.Generic;
using System.Text;

namespace POS.DAL.Models
{
    public class MasterCustomer
    {
        public string Id { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }

        public virtual ICollection<POSMain> POSMains { get; set; }
    }
}
