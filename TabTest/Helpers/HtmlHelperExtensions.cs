using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using TabTest.Models;

namespace TabTest.Helpers
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString KnockoutHiddenFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            object bindings , object htmlAttributes = null)
        {
            var attribs = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            var bindingExpression = AnonymousObjectToBindingExpression(bindings);
            attribs.Add("data-bind", bindingExpression);

            return htmlHelper.HiddenFor(expression, attribs);
        }

        public static MvcHtmlString KnockoutDropdownList(this HtmlHelper htmlHelper, object bindings, object htmlAttributes = null)
        {
            var tag = new TagBuilder("select");

            var attribs = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            var bindingExpression = AnonymousObjectToBindingExpression(bindings);
            attribs.Add("data-bind", bindingExpression);
            
            tag.MergeAttributes(attribs);

            return new MvcHtmlString(tag.ToString());
        }

        public static MvcHtmlString KnockoutDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList,
            string databindExpression, object htmlAttributes = null)
        {
            var attribs = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            attribs.Add("data-bind", databindExpression);

            return htmlHelper.DropDownListFor(expression, selectList, attribs);
        }

        public static MvcHtmlString KnockoutDisplayFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, string databindExpression, object htmlAttributes = null)
        {
            var attribs = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            var tagBuilder = new TagBuilder("span");

            tagBuilder.MergeAttribute("class", "display single-line");
            tagBuilder.MergeAttribute("data-bind", databindExpression);
            tagBuilder.MergeAttributes(attribs);
            var display = htmlHelper.DisplayFor(expression);
            tagBuilder.InnerHtml = display.ToHtmlString();

            return new MvcHtmlString(tagBuilder.ToString());
        }

        public static MvcHtmlString KnockoutTextboxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, 
            Expression<Func<TModel, TProperty>> expression, object bindings, object htmlAttributes = null)
        {
            var bindingExpression = AnonymousObjectToBindingExpression(bindings);
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            attributes.Add("data-bind", bindingExpression);

            return htmlHelper.TextBoxFor(expression, attributes);
        }

        public static MvcHtmlString KnockoutTextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, int rows, int cols, object bindings, object htmlAttributes = null)
        {
            var bindingExpression = AnonymousObjectToBindingExpression(bindings);
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            attributes.Add("data-bind", bindingExpression);

            return htmlHelper.TextAreaFor(expression, rows, cols, attributes);
        }

        public static MvcHtmlString KnockoutTextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, object bindings, object htmlAttributes = null)
        {
            var bindingExpression = AnonymousObjectToBindingExpression(bindings);
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            attributes.Add("data-bind", bindingExpression);

            return htmlHelper.TextAreaFor(expression, attributes);
        }

        public static MvcHtmlString KnockoutModelFor<TModel>(this HtmlHelper<TModel> htmlHelper, 
            object model, bool propertiesOnly, object htmlAttributes = null)
        {
            return new MvcHtmlString(BuildObject(model));
        }

        public static MvcHtmlString Tabs<TModel>(this HtmlHelper<TModel> htmlHelper, ControllerContext context,
            string id,
            IEnumerable<TabListItem> tabList,
            IDictionary<string, object> htmlAttributes = null)
        {
            return Tabs(htmlHelper, context, id, tabList, (object) htmlAttributes);
        }

        public static MvcHtmlString Tabs<TModel>(this HtmlHelper<TModel> htmlHelper, ControllerContext context,
            string id,
            IEnumerable<TabListItem> tabList,
            object htmlAttributes = null)
        {
            // create a div container
            // grab the template and go from there
            if (tabList == null)
                throw new ArgumentNullException("tabList");

            var list = tabList.ToList();

            var builder = new TagBuilder("div");

            builder.MergeAttribute("id", id);

            if (htmlAttributes != null)
            {
                builder.MergeAttributes(new RouteValueDictionary(htmlAttributes), false);
            }

            var htmlBuilder = new StringBuilder();

            htmlBuilder.Append(BuildTabHeaders(id, list));
            htmlBuilder.Append(BuildTabBodies(context, id, list));

            builder.InnerHtml = htmlBuilder.ToString();
            return new MvcHtmlString(builder.ToString());
        }

        public static bool IsGenericList(this object o)
        {
            var isGenericList = false;

            var oType = o.GetType();

            if (oType.IsGenericType && (oType.GetGenericTypeDefinition() == typeof(List<>)))
                isGenericList = true;

            return isGenericList;
        }

        private static string AnonymousObjectToBindingExpression(object bindings)
        {
            var bindingMap = HtmlHelper.AnonymousObjectToHtmlAttributes(bindings);

            var builder = new StringBuilder();
            foreach (var key in bindingMap.Keys)
            {
                if (builder.Length != 0)
                    builder.Append(",");

                builder.AppendFormat("{0}: {1}", key, bindingMap[key]);
            }

            return builder.ToString();
        }

        private static string BuildObject(object model, bool useObservables = true)
        {

            var builder = new StringBuilder();

            var type = model.GetType();
            var properties = type
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => Attribute.IsDefined(p, typeof(KnockoutAttribute))).ToList();

            foreach (var property in properties)
            {
                var attribute = (KnockoutAttribute)Attribute.GetCustomAttribute(property, typeof(KnockoutAttribute));

                var name = property.Name;

                if (attribute.PropertyName != null)
                    name = attribute.PropertyName;

                if (builder.Length != 0)
                    builder.Append(",\n");

                builder.Append(name);
                builder.Append(":");

                var value = property.GetValue(model);

                if (property.PropertyType.IsNumericType())
                {
                    builder.Append(value);
                }
                else if (property.PropertyType.IsAssignableFrom(typeof(string)))
                {
                    builder.AppendFormat(useObservables ? "ko.observable('{0}')" : "'{0}'", value);
                }
                else if (property.PropertyType.IsArray)
                {
                    builder.Append(BuildArray(value));
                } else if (value.IsGenericList())
                {
                    builder.AppendFormat("ko.observableArray([");
                    builder.Append(BuildGenericList(value));
                    builder.Append("])");
                }
                else
                {
                    // do an object instead
                    builder.Append(BuildObject(value));
                }
            }

            return builder.ToString();
        }

        private static string BuildGenericList(object listValue)
        {
            var list = listValue as IList;

            if (list == null) return null;

            var builder = new StringBuilder();
            foreach (var val in list)
            {
                if (builder.Length != 0) builder.Append(",");
                builder.Append("{");
                builder.Append(BuildObject(val, false));
                builder.Append("}");
            }

            return builder.ToString();
        }

        private static string BuildArray(object arrayValue)
        {
            var array = arrayValue as Array;

            return "";
        }

        private static string BuildTabBodies(ControllerContext context, string id, IEnumerable<TabListItem> list)
        {
            var stringBuilder = new StringBuilder();

            var count = 0;
            foreach (var item in list)
            {
                var divBuilder = new TagBuilder("div");
                divBuilder.MergeAttribute("id", string.Format("{0}_{1}", id, count));

                divBuilder.InnerHtml = RenderViewToString(context, item.TemplatePath, item.Model, item.IsPartial);
                count++;

                stringBuilder.Append(divBuilder);
            }

            return stringBuilder.ToString();
        }

        private static string BuildTabHeaders(string id, IEnumerable<TabListItem> tabList)
        {
            var builder = new TagBuilder("ul");

            var liList = new List<TagBuilder>();

            var count = 0;
            foreach (var item in tabList.ToList())
            {
                var liBuilder = new TagBuilder("li");
                var anchorBuilder = new TagBuilder("a");

                anchorBuilder.MergeAttribute("href", string.Format("#{0}_{1}", id, count));
                anchorBuilder.SetInnerText(item.TabHeader);

                liBuilder.InnerHtml = anchorBuilder.ToString();
                liList.Add(liBuilder);

                count++;
            }

            var stringBuilder = new StringBuilder();
            foreach (var li in liList)
            {
                stringBuilder.Append(li);
            }

            builder.InnerHtml = stringBuilder.ToString();

            return builder.ToString();
        }

        private static string RenderViewToString(ControllerContext context,
            string viewPath,
            object model = null,
            bool partial = false)
        {
            // first find the ViewEngine for this view
            var viewEngineResult = partial ? 
                                            ViewEngines.Engines.FindPartialView(context, viewPath) : 
                                            ViewEngines.Engines.FindView(context, viewPath, null);

            if (viewEngineResult == null || viewEngineResult.View == null)
                throw new FileNotFoundException("View cannot be found.");

            // get the view and attach the model to view data
            var view = viewEngineResult.View;
            context.Controller.ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var ctx = new ViewContext(context, view,
                    context.Controller.ViewData,
                    context.Controller.TempData,
                    sw);

                view.Render(ctx, sw);

                return sw.ToString();
            }
        }
    }
}