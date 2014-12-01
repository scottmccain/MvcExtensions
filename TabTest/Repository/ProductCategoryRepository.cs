using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mapping;
using NorthwindData;
using TabTest.Models;

namespace TabTest.Repository
{
    public class ProductCategoryRepository
    {
        private IMapper _mapper = new AutoMapperMapper();
        public IEnumerable<ProductCategoryModel> GetCategories()
        {
            using (var ctx = new AdventureWorksLT2008R2Entities())
            {
                return _mapper.Map<List<ProductCategoryModel>>(ctx.ProductCategories.ToList());
            }
        }
    }
}