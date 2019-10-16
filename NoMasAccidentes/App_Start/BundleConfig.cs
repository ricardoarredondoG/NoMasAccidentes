using System.Web;
using System.Web.Optimization;

namespace NoMasAccidentes
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //Login

            bundles.Add(new ScriptBundle("~/login/bundles/js").Include(
                        "~/Scripts/jquery-3.2.1.min.js",
                        "~/Scripts/materialize.min.js",
                        "~/Scripts/rut.js"));


            bundles.Add(new StyleBundle("~/login/Content/css").Include(
                      "~/Content/materialize.min.css",
                      "~/Content/paginaInicioAdmin.css",
                      "~/Content/paginaInicioCliente.css"));

            //Home

            bundles.Add(new StyleBundle("~/home/Content/css").Include(
                      "~/Content/materialize.min.css",
                      "~/Content/jquery.mCustomScrollbar.css",
                      "~/Content/material-design-iconic-font.min.css",
                      "~/Content/normalize.css",
                      "~/Content/style.css",
                      "~/Content/sweetalert.css"));

            bundles.Add(new ScriptBundle("~/home/bundles/js").Include(
                        "~/Scripts/jquery-2.2.0.min.js",
                        "~/Scripts/main.js",
                        "~/Scripts/materialize.min.js",
                        "~/Scripts/sweetalert.min.js"));


            //Factura
            bundles.Add(new StyleBundle("~/factura/Content/css").Include(
                      "~/Content/styleFactura.css"));

        }
    }
}
