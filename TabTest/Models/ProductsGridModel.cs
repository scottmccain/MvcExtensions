using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TabTest.Models
{
    public class ProductsGridModel
    {
        public int TotalRows
        {
            get; set;
        }

        public IEnumerable<ProductModel> Rows { get; set; }
    }
}