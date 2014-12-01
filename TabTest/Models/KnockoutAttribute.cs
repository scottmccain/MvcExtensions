using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace TabTest.Models
{
    public class KnockoutAttribute : Attribute
    {
        public string PropertyName { get; set; }
    }
}
