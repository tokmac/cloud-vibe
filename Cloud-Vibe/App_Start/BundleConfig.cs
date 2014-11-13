﻿using System.Web;
using System.Web.Optimization;

namespace Cloud_Vibe
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterScripts(bundles);

            RegisterStyles(bundles);
            

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = false;
        }

        private static void RegisterScripts(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/vendor/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/respond").Include(
                      "~/Scripts/Scripts/vendor/html5shiv.js",
                      "~/Scripts/vendor/respond.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/flat-ui-pro").Include(
                      "~/Scripts/flat-ui-pro.js",
                      "~/Scripts/vendor/respond.min.js"));
        }

        private static void RegisterStyles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/flat-ui-pro.css",
                      "~/Content/site.css"));
        }
    }
}
