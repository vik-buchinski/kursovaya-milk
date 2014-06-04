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
                            .Include("~/Scripts/jquery.js")
                            .Include("~/Scripts/bootstrap-carousel.js")
                            .Include("~/Scripts/bootstrap-transitions.js")
                            .Include("~/Scripts/bootstrap-collapse.js")
                            .Include("~/Scripts/jquery.unobtrusive-ajax.js")
                            .Include("~/Scripts/jquery.validate.js")
                            .Include("~/Scripts/jquery.validate.unobtrusive.js")
                            .Include("~/Scripts/app.js")
                            .Include("~/Scripts/client.js"));
        }
    }
}