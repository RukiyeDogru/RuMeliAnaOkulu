using anaokulumvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace anaokulumvc.Ayarlar
{
    public class _SecurityFilter : ActionFilterAttribute
    {
        string yetkili;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string ControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            if (HttpContext.Current.Session["kullanici"] == null && ControllerName != "Login" && ControllerName != "RM" )
            {
                filterContext.Result = new RedirectResult("/Login/Index");
                return;
            }
            if (HttpContext.Current.Session["kullanici"] != null)
            {
                kullanici k = (kullanici)HttpContext.Current.Session["kullanici"];
                if (k.yetkiID == 1)
                {
                    yetkili = "Müdür";
                }
                if (yetkili != "Müdür" && (ControllerName == "Müdür" || ControllerName == "ogretmen"))
                {
                    filterContext.Result = new RedirectResult("/Talep/Index");
                    return;
                }
            }


            base.OnActionExecuting(filterContext);
        }

    }
}