using System.Web.Mvc;

namespace WebApplication3.Areas.mangeteacher
{
    public class mangeteacherAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "mangeteacher";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "mangeteacher_default",
                "mangeteacher/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}