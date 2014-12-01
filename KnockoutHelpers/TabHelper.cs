using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace KnockoutHelpers
{
    public static class TabHelper
    {
        public static MvcHtmlString Tabs<TModel>(this HtmlHelper<TModel> htmlHelper, ControllerContext context,
            IEnumerable<TabListItem> tabList, string id,
            IDictionary<string, object> htmlAttributes = null)
        {
            return Tabs(htmlHelper, context, tabList, id, (object)htmlAttributes);
        }

        public static MvcHtmlString Tabs<TModel>(this HtmlHelper<TModel> htmlHelper, ControllerContext context,
            IEnumerable<TabListItem> tabList, string id,
            object htmlAttributes = null)
        {
            // create a div container
            // grab the template and go from there
            if (tabList == null)
                throw new ArgumentNullException("tabList");

            var list = tabList.ToList();

            var builder = new TagBuilder("div");

            //var id = "tabs" + Guid.NewGuid().ToString().Replace('-', '_');

            builder.MergeAttribute("id", id);
            if (htmlAttributes != null)
            {
                builder.MergeAttributes(new RouteValueDictionary(htmlAttributes), false);
            }
            builder.AddCssClass("tabs");

            var htmlBuilder = new StringBuilder();

            htmlBuilder.Append(BuildTabHeaders(id, list));
            htmlBuilder.Append(BuildTabBodies(context, id, list));

            builder.InnerHtml = htmlBuilder.ToString();
            return new MvcHtmlString(builder.ToString());
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

        private static string BuildTabBodies(ControllerContext context, string id, IEnumerable<TabListItem> list)
        {
            var stringBuilder = new StringBuilder();

            var container = new TagBuilder("div");
            container.AddCssClass("tab-content");

            var count = 0;
            foreach (var item in list)
            {
                var divBuilder = new TagBuilder("div");
                divBuilder.MergeAttribute("id", string.Format("{0}_{1}", id, count));

                //if(count == 0) divBuilder.AddCssClass("active");

                //divBuilder.AddCssClass("tab");

                divBuilder.InnerHtml = RenderViewToString(context, item.TemplatePath, item.Model, item.IsPartial);
                count++;

                stringBuilder.Append(divBuilder);
            }

            container.InnerHtml = stringBuilder.ToString();
            return container.ToString();
        }

        private static string BuildTabHeaders(string id, IEnumerable<TabListItem> tabList)
        {
            var builder = new TagBuilder("ul");
            //builder.AddCssClass("tab-links");

            var liList = new List<TagBuilder>();

            var count = 0;
            foreach (var item in tabList.ToList())
            {
                var liBuilder = new TagBuilder("li");
                var anchorBuilder = new TagBuilder("a");

                //if(count == 0) liBuilder.AddCssClass("active");

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

    }

}
