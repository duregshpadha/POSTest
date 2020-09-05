using System;
using System.Collections.Generic;
using System.Text;

namespace POS.BAL.Models
{
    public class Select2Model
    {
        public string Id { get; set; }
        public string Text { get; set; }
    }

    public class Select2Pagination
    {
        public bool More { get; set; }
    }

    public class Select2ModelResult
    {
        public List<Select2Model> Results { get; set; }
        public Select2Pagination Pagination { get; set; }
    }
}
