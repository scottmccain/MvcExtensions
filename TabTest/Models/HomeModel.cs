using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KnockoutHelpers;
using TabTest.Helpers;

namespace TabTest.Models
{
    public class HomeModel
    {
        public List<TabListItem> TabList { get; set; }

        [Knockout]
        public ProductTabModel ProductTabModel { get; set; }

        [Knockout]
        public ProductCategoryTabModel ProductCategoryTabModel { get; set; }

    }
}