using System.Web;
using System.Web.Optimization;

namespace TabTest
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/libraries").Include(
                        "~/Scripts/libraries/yahoo.js",
                        "~/Scripts/libraries/cssQuery-p.js", 
                        "~/Scripts/libraries/event.js",
                        "~/Scripts/libraries/chdp.js"));

            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                "~/Scripts/knockout-2.2.0.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));
            
            bundles.Add(new StyleBundle("~/bundles/tabs").Include(
                        "~/Content/tabs.css"));

            bundles.Add(new ScriptBundle("~/bundles/syncfusion").Include(
                "~/Scripts/jquery.easing.{version}.js",
                "~/Scripts/jquery.globalize.js",
                "~/Scripts/jsrender.js",
                "~/Scripts/ej.web.all.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jqwidgets").Include(
                "~/Scripts/jqwidgets/jqxcore.js",
                "~/Scripts/jqwidgets/jqxdata.js",
                "~/Scripts/jqwidgets/jqxbuttons.js",
                "~/Scripts/jqwidgets/jqxscrollbar.js",
                "~/Scripts/jqwidgets/jqxmenu.js",
                "~/Scripts/jqwidgets/jqxgrid.js",
                "~/Scripts/jqwidgets/jqxgrid.pager.js",
                "~/Scripts/jqwidgets/jqxgrid.sort.js",
                "~/Scripts/jqwidgets/jqxgrid.selection.js",
                "~/Scripts/jqwidgets/jqxgrid.filter.js",
                "~/Scripts/jqwidgets/jqxcheckbox.js",
                "~/Scripts/jqwidgets/jqxgrid.columnsreorder.js",
                "~/Scripts/jqwidgets/jqxgrid.edit.js",
                "~/Scripts/jqwidgets/jqxnumberinput.js",
                "~/Scripts/jqwidgets/jqxlistbox.js",
                "~/Scripts/jqwidgets/jqxdropdownlist.js",
                "~/Scripts/jqwidgets/jqxgrid.selection.js",
                "~/Scripts/jqwidgets/jqxgrid.columnsresize.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-1.11.2.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqGrid").Include(
                        "~/Scripts/jquery.jqGrid.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquerytabs").Include(
                        "~/Scripts/jquery.tabify.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/site.css", 
                        "~/Content/tabs.css", 
                        "~/Content/rollup.css", 
                        "~/Content/page.css"));

            bundles.Add(new StyleBundle("~/Content/jquery-ui/css").Include(
                        "~/Content/jquery-ui/jquery-ui.css"));

            bundles.Add(new StyleBundle("~/Content/syncfusion/css").Include(
                        "~/Content/syncfusion/bootstrap.css",
                        "~/Content/syncfusion/default-theme/ej.widgets.all.css",
                        "~/Content/syncfusion/responsive-css/ej.responsive.css"));

            
            bundles.Add(new StyleBundle("~/Content/jqwidgets/css").Include(
                "~/Content/jqwidgets/styles/jqx.base.css",
                "~/Content/jqwidgets/styles/jqx.energyblue.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}