using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TabTest.Models
{
    public class ProductCategoryModel
    {
        public int ProductCategoryID { get; set; }
        public ProductCategoryModel Parent { get; set; }
        public string Name { get; set; }
    }
}