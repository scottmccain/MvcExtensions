using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace KnockoutHelpers
{
    public static class DropdownHelper
    {
        public static MvcHtmlString KnockoutDropdownList(this HtmlHelper htmlHelper, object bindings, object htmlAttributes = null)
        {
            var tag = new TagBuilder("select");

            var attribs = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            var bindingExpression = ObjectHelper.AnonymousObjectToBindingExpression(bindings);
            attribs.Add("data-bind", bindingExpression);

            tag.MergeAttributes(attribs);

            return new MvcHtmlString(tag.ToString());
        }

        public static MvcHtmlString KnockoutDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList,
            object bindings, object htmlAttributes = null)
        {
            var attribs = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            var databindExpression = ObjectHelper.AnonymousObjectToBindingExpression(bindings);
            attribs.Add("data-bind", databindExpression);

            return htmlHelper.DropDownListFor(expression, selectList, attribs);
        }


    }
}
