using QuanLyNhanVien_Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyNhanVien_Client.Authorize
{
    public class AuthorizePosition:AuthorizeAttribute
    {
        public string TenChucVu { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            NhanVien_PhongBan objNhanVien_PhongBan = (NhanVien_PhongBan)HttpContext.Current.Session["EmployeeCurrent"];
            if (objNhanVien_PhongBan != null)
            {

                int maNhanVien = objNhanVien_PhongBan.MaNhanVien;

                int maChucVu = objNhanVien_PhongBan.MaChucVu;

                ChucVuClient chucVuClient = new ChucVuClient();

                //Lay tat ca cac chuc vu hien co trong database
                IEnumerable<ChucVu> lstChucVu = chucVuClient.FindALL();

                var tenChucVu = lstChucVu.Where(x => x.MaChucVu == maChucVu).FirstOrDefault().TenChucVu;

                if (tenChucVu == TenChucVu)
                {
                    return true;
                }
            }
            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new ViewResult()
            {
                ViewName = "~/Views/Shared/AuthorizeFailed.cshtml"
            };
        }
    }
}