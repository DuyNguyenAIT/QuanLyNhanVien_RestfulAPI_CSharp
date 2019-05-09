using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyNhanVien_Client
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CustomAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        public string ViewName { get; set; }
        public CustomAuthorizeAttribute()
        {
            ViewName = "AuthorizeFailed";
        }
        void IsUserAuthorized(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                return;
            }
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                ViewDataDictionary dic = new ViewDataDictionary();
                dic.Add("Message", "Bạn không đủ quyền coi nội dung này!! Out hộ");
                var result = new ViewResult() { ViewName = this.ViewName, ViewData = dic };
                filterContext.Result = result;

            }
        }
    }

}