using System.Text;
using System.Web.Mvc;

namespace KnockoutHelpers
{
    class ObjectHelper
    {
        public static string AnonymousObjectToBindingExpression(object bindings)
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
    }
}
