using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KnockoutHelpers;

namespace TabTest.Models
{
    public class ProductCategoryTabModel
    {
        [Knockout(PropertyName = "text")]
        public string SomeText
        {
            get;
            set;
        }

        [Knockout(PropertyName = "selectedItem")]
        public string SelectedItem { get; set; }

        [Knockout(PropertyName = "items")]
        public IList<Department> Departments
        {
            get;
            set;
        }

        public IList<SelectListItem> Items
        {
            get;
            set;
        }
    }
}