using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using NorthwindData;

namespace TabTest.Repository.Filters
{
    public class GridFilter
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string SortField { get; set; }
        public FilterSortOrder SortOrder { get; set; }
        public List<ColumnFilter> Filters { get; set; }

    }
}