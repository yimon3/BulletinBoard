using System.Web.Optimization;

namespace BulletinBoardSampleFrame
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Resources/lib/script/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Resources/lib/script/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Resources/lib/script/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Resources/lib/script/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Resources/lib/content/bootstrap.css",
                      "~/Resources/lib/content/Content/site.css"));
        }
    }
}
