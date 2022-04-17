using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.filters
{
    public class CustomAuthorizationFilterAttribute : FilterAttribute, IAuthorizationFilter
    {
        private string _loginUrl = null;
        public CustomAuthorizationFilterAttribute(string loginUrl= "~/LoginWindow/Login/Logindex")
        {
            this._loginUrl = loginUrl;
        }
        /// <summary>
        /// 当需要验证权限的时候进来
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnAuthorization(AuthorizationContext filterContext)
        {

            var httpContext = filterContext.HttpContext;   //能拿到httpContxet,就可以为所欲为
            //if(filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(CustomAuthorizationFilterAttribute)))
            if((httpContext.Session["currentuser"] is tb_Admin)|| (httpContext.Session["currentuser"] is tb_student)|| (httpContext.Session["currentuser"] is tb_teacher))
            {
                if((httpContext.Session["currentuser"] is tb_Admin))
                {
                    tb_Admin userlogin = (tb_Admin)httpContext.Session["currentuser"];
                    return;
                }
                else if ((httpContext.Session["currentuser"] is tb_student))
                {
                    tb_student userlogin = (tb_student)httpContext.Session["currentuser"];
                    return;
                }
                else
                {
                    tb_teacher userlogin = (tb_teacher)httpContext.Session["currentuser"];
                    return;
                }


            }
            else
            {
                if (httpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new JsonResult()
                    {
                        Data = new
                        {
                            DebugMessage = "登录过期",
                            retValue = ""
                        }
                    };
                }
                else
                {
                    httpContext.Session["currentUrl"] = httpContext.Request.Url.AbsoluteUri;
                    filterContext.Result = new RedirectResult(this._loginUrl);
                }
            }
        }
    }
}