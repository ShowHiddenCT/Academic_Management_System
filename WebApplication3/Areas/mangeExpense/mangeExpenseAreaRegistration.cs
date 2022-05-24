using System.Web.Mvc;

namespace WebApplication3.Areas.mangeExpense
{
    public class mangeExpenseAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "mangeExpense";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "mangeExpense_default",
                "mangeExpense/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}