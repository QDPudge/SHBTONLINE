using System.Web;
using System.Web.Optimization;

namespace SHBTONLINE
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/Layuicss").Include(
                      "~/Content/layui/css/layui.css",
                      "~/Content/layui/css/layui.mobilecss",
                      "~/Content/layui/css/modules/laydate/default/layui.css"));

            bundles.Add(new ScriptBundle("~/Content/LayuiJS").Include(
                      "~/Content/layui/layui.js",
                      "~/Content/layui/lay/modules/carousel.js",
                      "~/Content/layui/lay/modules/code.js",
                      "~/Content/layui/lay/modules/element.js",
                      "~/Content/layui/lay/modules/flow.js",
                      "~/Content/layui/lay/modules/form.js",
                      "~/Content/layui/lay/modules/laydate.js",
                      "~/Content/layui/lay/modules/layedit.js",
                      "~/Content/layui/lay/modules/layer.js",
                      "~/Content/layui/lay/modules/laypage.js",
                      "~/Content/layui/lay/modules/laytpl.js",
                      "~/Content/layui/lay/modules/mobile.js",
                      "~/Content/layui/lay/modules/table.js",
                      "~/Content/layui/lay/modules/tree.js",
                      "~/Content/layui/lay/modules/upload.js",
                      "~/Content/layui/lay/modules/util.js"));
        }
    }
}
