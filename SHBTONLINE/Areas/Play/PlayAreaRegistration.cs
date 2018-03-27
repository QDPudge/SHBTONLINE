using System.Web.Mvc;

namespace SHBTONLINE.Areas.Play
{
    public class PlayAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Play";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Play_default",
                "Play/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}