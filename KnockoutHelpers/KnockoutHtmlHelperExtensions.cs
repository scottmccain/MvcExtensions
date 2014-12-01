using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace KnockoutHelpers
{
    public static class KnockoutHtmlHelperExtensions
    {
        public static MvcHtmlString KnockoutHiddenFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            object bindings , object htmlAttributes = null)
        {
            var attribs = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            var bindingExpression = ObjectHelper.AnonymousObjectToBindingExpression(bindings);
            attribs.Add("data-bind", bindingExpression);

            return htmlHelper.HiddenFor(expression, attribs);
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
            var bindingExpression = ObjectHelper.AnonymousObjectToBindingExpression(bindings);
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            attributes.Add("data-bind", bindingExpression);

            return htmlHelper.TextBoxFor(expression, attributes);
        }

        public static MvcHtmlString KnockoutTextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, int rows, int cols, object bindings, object htmlAttributes = null)
        {
            var bindingExpression = ObjectHelper.AnonymousObjectToBindingExpression(bindings);
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            attributes.Add("data-bind", bindingExpression);

            return htmlHelper.TextAreaFor(expression, rows, cols, attributes);
        }

        public static MvcHtmlString KnockoutTextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, object bindings, object htmlAttributes = null)
        {
            var bindingExpression = ObjectHelper.AnonymousObjectToBindingExpression(bindings);
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            attributes.Add("data-bind", bindingExpression);

            return htmlHelper.TextAreaFor(expression, attributes);
        }

        public static MvcHtmlString KnockoutModelFor<TModel>(this HtmlHelper<TModel> htmlHelper, 
            object model, bool propertiesOnly, object htmlAttributes = null)
        {
            return new MvcHtmlString(BuildObject(model));
        }


        public static bool IsGenericList(this object o)
        {
            var isGenericList = false;

            var oType = o.GetType();

            if (oType.IsGenericType && (oType.GetGenericTypeDefinition() == typeof(List<>)))
                isGenericList = true;

            return isGenericList;
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
                    builder.AppendFormat("ko.observableArray([");
                    builder.Append(BuildArray(value));
                    builder.Append("])");
                }
                else if (value.IsGenericList())
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

            if (array == null) return "";

            var stringBuilder = new StringBuilder();
            foreach (var value in array)
            {
                if (stringBuilder.Length > 0)
                    stringBuilder.Append(",");

                stringBuilder.Append("{");
                stringBuilder.Append(BuildObject(value, false));
                stringBuilder.Append("}");
            }

            return stringBuilder.ToString();
        }


    }
}