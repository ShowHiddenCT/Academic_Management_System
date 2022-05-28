using System.Web.Mvc;

namespace WebApplication3.Areas.major
{
    public class majorAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "major";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "major_default",
                "major/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}