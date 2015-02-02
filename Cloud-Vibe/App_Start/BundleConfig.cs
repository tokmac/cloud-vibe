using System.Web;
using System.Web.Optimization;

namespace Cloud_Vibe
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();

            RegisterScripts(bundles);
            RegisterStyles(bundles);

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = false;
        }

        private static void RegisterScripts(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/kendo")
                .Include("~/Scripts/kendo/kendo.all.min.js")
                .Include("~/Scripts/kendo/kendo.aspnetmvc.min.js")
                .Include("~/Scripts/kendo/kendo.web.js")
                .Include("~/Scripts/kendo/cultures/kendo.culture.bg-BG.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery")
                .Include("~/Scripts/kendo/jquery.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"
                        ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/respond").Include(
                      "~/Scripts/Scripts/vendor/html5shiv.js",
                      "~/Scripts/vendor/respond.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/flat-ui-pro").Include(
                      "~/Scripts/flat-ui-pro.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-mixitup")
                .Include("~/Scripts/codrops.captionhovereffect/toucheffects.js")
                .Include("~/Scripts/codrops.captionhovereffect/modernizr.custom.js")
                .Include("~/Scripts/jquery.mixitup/jquery.mixitup.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-avgrund").Include(
                      "~/Scripts/jquery.avgrund/jquery.avgrund.js"));
        }

        private static void RegisterStyles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                "~/Content/bootstrap.min.css"
                ));

            bundles.Add(new StyleBundle("~/Content/kendo").Include(
                "~/Content/kendo/kendo.common-bootstrap.min.css",
                "~/Content/kendo/kendo.bootstrap.min.css",
                "~/Content/kendo/kendo.common.min.css"
                ));

            bundles.Add(new StyleBundle("~/Content/font-awesome").Include(
                    "~/Content/font-awesome.css",
                    "~/Content/bootstrap-social.css"
                ));

            bundles.Add(new StyleBundle("~/Content/flat-ui").Include(
                    "~/Content/flat-ui-pro.css"
                ));

            bundles.Add(new StyleBundle("~/Content/jquery-avgrund").Include(
                    "~/Content/avgrund.css"
                ));

            bundles.Add(new StyleBundle("~/Content/codrops-captionhovereffect").Include(
                    "~/Content/codrops.captionhovereffect/component.css"
                ));

            bundles.Add(new StyleBundle("~/Content/custom").Include(
                    "~/Content/Site.css"
                ));
        }
    }
}
