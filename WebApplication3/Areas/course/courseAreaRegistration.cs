using System.Web.Mvc;

namespace WebApplication3.Areas.course
{
    public class courseAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "course";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "course_default",
                "course/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}