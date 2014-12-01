using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TabTest.Models
{
    public class ProductModel
    {
        public string Name { get; set; }
        public decimal StandardCost { get; set; }
        public string Color { get; set; }
        public string ProductNumber { get; set; }
    }
}