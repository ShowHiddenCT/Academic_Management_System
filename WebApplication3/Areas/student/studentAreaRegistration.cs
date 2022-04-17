using System.Web.Mvc;

namespace WebApplication3.Areas.student
{
    public class studentAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "student";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "student_default",
                "student/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}