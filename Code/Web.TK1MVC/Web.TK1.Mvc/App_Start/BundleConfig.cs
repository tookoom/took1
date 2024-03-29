﻿using System.Web;
using System.Web.Optimization;

namespace Web.TK1.Mvc
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            //CUSTOM SCRIPTS
            bundles.Add(new ScriptBundle("~/bundles/bizzscripts").Include(
                "~/Scripts/jquery.tn3lite.min.js", "~/Scripts/tk1.bizz.broker.js"));

            //CUSTOM SCRIPTS
            bundles.Add(new ScriptBundle("~/bundles/mapscripts").Include(
                "~/Scripts/tk1.maps.js"));

            //CUSTOM CSS
            bundles.Add(new StyleBundle("~/Content/bizzcss").Include(
                "~/Content/base.css", "~/Content/broker.css", "~/Content/brokerparts.css"));

            //CUSTOM CSS
            bundles.Add(new StyleBundle("~/Content/mapscss").Include(
                "~/Content/base.css", "~/Content/maps.css"));

            bundles.Add(new StyleBundle("~/Content/quakecss").Include(
                "~/Content/ThirdParty/QuakeSlider/quake.slider.css",
                "~/Content/ThirdParty/QuakeSlider/plain/quake.skin.css"));

            bundles.Add(new StyleBundle("~/Content/tn3css").Include(
                "~/Content/ThirdParty/tn3/tn3.css"));


            #region DEFAULT BUNDLES
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));



            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/base.css", "~/Content/site.css"));

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

            #endregion
        }
    }
}