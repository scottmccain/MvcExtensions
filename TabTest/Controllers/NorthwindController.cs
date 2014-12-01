using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TabTest.Helpers;
using TabTest.Models;
using TabTest.Repository;
using TabTest.Repository.Filters;

namespace TabTest.Controllers
{
    public class NorthwindController : Controller
    {
        //
        // GET: /Northwind/
        //private readonly AutoMapperMapper _mapper = new AutoMapperMapper();

        readonly ProductRepository _productRepository = new ProductRepository();

        public ActionResult AllProducts(int pagenum = 0, int pagesize = 0, string sortDataField = null, string sortOrder = null)
        {
            var filter = MakeFilter(pagenum, pagesize, sortDataField, sortOrder);

            var result = _productRepository.GetProductsFiltered(filter);
            return Json(new ProductsGridModel { TotalRows = result.Item1, Rows = result.Item2}, JsonRequestBehavior.AllowGet);
        }

        private GridFilter MakeFilter(int pagenum, int pagesize, string sortDataField, string sortOrder)
        {
            FilterSortOrder order;
            Enum.TryParse(sortOrder, true, out order);

            return new GridFilter
            {
                Page = pagenum,
                PageSize = pagesize,
                SortField = sortDataField,
                SortOrder = order,
                Filters = GetGridColumnFiltersFilters()
            };
        }

        private List<ColumnFilter> GetGridColumnFiltersFilters()
        {
            var list = new List<ColumnFilter>();

            int filterCount;
            (Request["filterscount"] ?? "").SafeCastTo(out filterCount);

            for (var i = 0; i < filterCount; i++)
            {
                var filterValue = Request["filtervalue" + i];
                var filterDataField = Request["filterdatafield" + i];

                var condition = Request["filtercondition" + i] ?? "";

                FilterCondition filterCondition;
                Enum.TryParse(condition.Replace("_", ""), true, out filterCondition);

                FilterOperator filterOperator;
                Enum.TryParse((Request["filteroperator" + i] ?? ""), true, out filterOperator);

                list.Add(new ColumnFilter
                {
                    Value = filterValue,
                    ColumnName = filterDataField,
                    Operator = filterOperator,
                    Condition = filterCondition
                });
            }


            return list;
        }

    }
}
