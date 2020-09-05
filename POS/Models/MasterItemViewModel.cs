using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Models
{
    public class MasterItemViewModel
    {
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [Required]
        public decimal Rate { get; set; }

        [Required]
        public int Stock { get; set; }
    }
}
