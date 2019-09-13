using System.Web;
using System.Web.Optimization;

namespace NoMasAccidentes
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                        "~/Scripts/jquery-3.2.1.min.js",
                        "~/Scripts/materialize.min.js",
                        "~/Scripts/rut.js"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/materialize.min.css",
                      "~/Content/paginaInicioAdmin.css",
                      "~/Content/paginaInicioCliente.css"));
        }
    }
}
