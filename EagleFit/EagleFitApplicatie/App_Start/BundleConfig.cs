﻿using System.Web.Optimization;

namespace EagleFitApplicatie
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));
           

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/decimalValidation.js"));
            bundles.Add(new ScriptBundle("~/bundles/mapjs").Include(
                        "~/Scripts/map.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap-pulse.css",
                      "~/Content/bootstrap.css"));
            bundles.Add(new ScriptBundle("~/bundles/LidMenu").Include(
                      "~/Scripts/lidGegevensMenu.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include("~/Scripts/jquery-ui-{version}.js"));
            bundles.Add(new ScriptBundle("~/Content/jqueryuicss").Include("~/Content/themes/base/jquery-ui.css"));

            bundles.Add(new ScriptBundle("~/bundles/datetime").Include(
                       "~/Scripts/moment*",
                       "~/Scripts/bootstrap-datetimepicker*"));
        }
    }
}
