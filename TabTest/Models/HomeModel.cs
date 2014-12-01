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
        public Tab1Model Tab1Model { get; set; }

        [Knockout]
        public Tab2Model Tab2Model { get; set; }

    }
}