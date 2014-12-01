using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KnockoutHelpers;

namespace TabTest.Models
{
    public class Department
    {
        [Knockout(PropertyName = "code")]
        public string Code { get; set; }

        [Knockout]
        public string Name
        {
            get; set;
        }
    }
}