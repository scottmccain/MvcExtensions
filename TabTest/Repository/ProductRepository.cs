using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Linq.DynamicQuery;
using System.Text;
using KnockoutHelpers;
using Mapping;
using NorthwindData;
using TabTest.Repository.Filters;
using ProductModel = TabTest.Models.ProductModel;

namespace TabTest.Repository
{
    public class ProductRepository
    {
        private readonly AutoMapperMapper _mapper = new AutoMapperMapper();

        private readonly Dictionary<string, Type> _columnDictionary = new Dictionary<string, Type>
        {
            { "ProductName", typeof(String) },
            { "ProductNumber", typeof(String) },
            { "Color", typeof(String) },
            { "StandardCost", typeof(decimal) }
        }; 

        public Tuple<int, IEnumerable<ProductModel>> GetProductsFiltered(GridFilter filter)
        {
            var skip = filter.Page * filter.PageSize;

            using (var ctx = new AdventureWorksLT2008R2Entities())
            {
                var productQuery = BuildQuery(ctx, filter);

                //var products = _mapper.Map<List<ProductModel>>(ctx.Products.ToList());

                var products = _mapper.Map<List<ProductModel>>(productQuery.ToList());
                return new Tuple<int, IEnumerable<ProductModel>>(products.Count(), products.Skip(skip).Take(filter.PageSize));
            }            
        }

        private IEnumerable<Product> BuildQuery(AdventureWorksLT2008R2Entities ctx, GridFilter filter)
        {
            var queryFilter =
                from ft in filter.Filters
                group ft by ft.ColumnName into newGroup
                orderby newGroup.Key
                select newGroup;

            IQueryable<Product> query = ctx.Products;

            foreach (var fieldGroup in queryFilter)
            {
                var expressionBuilder = new StringBuilder();

                expressionBuilder.Append("(");
                foreach (var field in fieldGroup)
                {
                    if (expressionBuilder.Length > 1)
                    {
                        switch (field.Operator)
                        {
                            case FilterOperator.Or:
                                expressionBuilder.Append(" || ");
                                break;
                            default:
                                expressionBuilder.Append(" && ");
                                break;
                        }
                    }

                    var expression = ExpressionBuilder.GetExpression(field.ColumnName, field.Condition, field.Value,
                        _columnDictionary[field.ColumnName]);

                    expressionBuilder.Append(expression);
                }

                expressionBuilder.Append(")");

                query = query.Where(expressionBuilder.ToString());
            }

            if (!string.IsNullOrEmpty(filter.SortField))
            {
                if (filter.SortOrder == FilterSortOrder.Desc)
                    query = query.OrderBy(filter.SortField + " DESC");
                else
                    query = query.OrderBy(filter.SortField);
            }

            return query;
        }

    }
}