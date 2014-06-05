using System.Web.Optimization;

namespace FrontEnd.App_Start
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterStyleBundles(bundles);
            RegisterJavascriptBundles(bundles);
        }

        private static void RegisterStyleBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/css")
                            .Include("~/Content/bootstrap.css")
                            .Include("~/Content/milk.css")
                            .Include("~/Content/milk-responsive.css")
                            .Include("~/Content/site.css")
                            .Include("~/Content/common.css"));

        }

        private static void RegisterJavascriptBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js")
                            .Include("~/Scripts/Libs/jquery.js")
                            .Include("~/Scripts/Libs/bootstrap-carousel.js")
                            .Include("~/Scripts/Libs/bootstrap-transitions.js")
                            .Include("~/Scripts/Libs/bootstrap-collapse.js")
                            .Include("~/Scripts/Libs/jquery.unobtrusive-ajax.js")
                            .Include("~/Scripts/Libs/jquery.validate.js")
                            .Include("~/Scripts/Libs/jquery.validate.unobtrusive.js")
                            .Include("~/Scripts/Libs/underscore.js")
                            .Include("~/Scripts/Libs/backbone.js")
                            .Include("~/Scripts/backbone-router.js")
                            .Include("~/Scripts/app.js")
                            .Include("~/Scripts/client.js"));
        }
    }
}