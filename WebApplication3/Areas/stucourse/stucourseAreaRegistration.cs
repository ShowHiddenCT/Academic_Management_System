using System.Web.Mvc;

namespace WebApplication3.Areas.stucourse
{
    public class stucourseAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "stucourse";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "stucourse_default",
                "stucourse/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}