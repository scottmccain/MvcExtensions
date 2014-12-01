using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TabTest.Helpers
{
    public class TabListItem
    {
        public string TabHeader
        {
            get; set;
        }

        public bool IsPartial
        {
            get; set;
        }

        public string TemplatePath
        {
            get; set;
        }

        public dynamic Model
        {
            get; set;
        }
    }
}