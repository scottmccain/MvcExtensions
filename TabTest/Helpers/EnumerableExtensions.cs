using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace TabTest.Helpers
{
    public static class EnumerableExtensions
    {
        public static IList<SelectListItem> MakeSelectList<TModel, TValueProperty, TDisplayProperty>(this IEnumerable<TModel> list,
            Func<TModel, TValueProperty> valueExpression, Func<TModel, TDisplayProperty> displayExpression, Func<TModel, bool> selectedExpression  )
        {
            return list.Select(item => new SelectListItem
            {
                Selected = selectedExpression(item),
                Text = displayExpression(item).ToString(),
                Value = valueExpression(item).ToString()
            }).ToList();
        }

        public static Dictionary<string, object> MakeDictionaryFromAnonymousObject(this object objFrom)
        {
            var type = objFrom.GetType();
            var props = type.GetProperties();
            return props.ToDictionary(x => x.Name, x => x.GetValue(objFrom, null));
        }
    }
}