﻿using System.Web.Optimization;

namespace RMS.Web.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //Old
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js",
            //            "~/Scripts/jquery-ui-{version}.js"));
            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));
            //bundles.Add(new ScriptBundle("~/bundles/AppScripts").Include(
            
            //   "~/Scripts/App/datepicker.js"
            //New Version
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                     "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
               "~/Scripts/respond.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                         "~/Scripts/jquery-ui-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/AppScripts").Include(
             "~/Scripts/AppScripts/ajax.js",
            "~/Scripts/AppScripts/jquery.alert-confirm.js",
            "~/Scripts/AppScripts/common.js",
              "~/Scripts/showLoading/jquery.showLoading.js",
              "~/Scripts/App/datepicker.js"
                // "~/Scripts/AppScripts/DropdownMenu.js"
            ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));
            //   bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //    "~/Scripts/bootstrap.js",
            //     "~/Scripts/respond.js"));
            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //    "~/Content/bootstrap.min.css",
            //     "~/Content/themes/base/minified/jquery-ui.min.css",
            //          "~/Content/Site.css",
            //           "~/Content/AppStyles/FormControlStyle.css",
            //          "~/Content/AppStyles/webGridScript.css",
            //             "~/Content/AppStyles/DropdownMenu.css",
            //          "~/Scripts/showLoading/css/showLoading.css"

            //New Version
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                 "~/Content/themes/base/minified/jquery-ui.min.css",
                      "~/Content/Site.css",
                       "~/Content/bootstrap.css",
                       "~/Content/AppStyles/FormControlStyle.css",
                      "~/Content/AppStyles/webGridScript.css",
                         "~/Content/AppStyles/DropdownMenu.css",
                      "~/Scripts/showLoading/css/showLoading.css"
                     ));
        }
    }
}
