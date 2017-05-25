using System.Web;
using System.Web.Optimization;

namespace PMT.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/moment-with-locales.js",
                      "~/Scripts/bootstrap-datepicker.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-datepicker.css",
                      "~/Content/font-awesome.css"));

            bundles.Add(new StyleBundle("~/css").Include(
                      "~/css/whhg.css",
                      "~/css/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/handlebars").Include(
                      "~/Scripts/handlebars.js"));
            bundles.Add(new ScriptBundle("~/bundles/chart").Include(
                      "~/Scripts/Chart.js"));


            bundles.Add(new ScriptBundle("~/js/app").Include(
                        "~/js/App.CommonUI.js",
                        "~/js/App.MoneyAccountUI.js",
                        "~/js/App.TransactionsUI.js",
                        "~/js/App.TransactionsFiltersUI.js",
                        "~/js/App.CategoriesUI.js",
                        "~/js/App.CategoriesCreateUI.js",
                        "~/js/App.HomeCharts.js",
                        "~/js/App.js"
                        ));
        }
    }
}
