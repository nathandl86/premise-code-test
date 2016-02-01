using System.Web.Optimization;

namespace DutyHours
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/vendor/jquery/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/vendor/bootstrap/bootstrap.js",
                      "~/Scripts/vendor/respond/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                    "~/Scripts/vendor/angular/angular.js",
                    "~/Scripts/vendor/angular-ui-bootstrap/ui-bootstrap.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/lodash").Include(
                    "~/Scripts/vendor/lodash/lodash.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/toastr").Include(
                    "~/Scripts/vendor/toastr/toastr.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                    "~/Scripts/app/app.js",
                    "~/Scripts/app/init.js",
                    "~/Scripts/app/services/*.service.js",
                    "~/Scripts/app/directives/*.directive.js",
                    "~/Scripts/app/controllers/*.controller.js"
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/toastr.min.css",
                      "~/Content/site.css"));
        }
    }
}
