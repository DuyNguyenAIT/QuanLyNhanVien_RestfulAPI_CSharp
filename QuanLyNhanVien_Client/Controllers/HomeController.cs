using QuanLyNhanVien_Client.Filter;
using QuanLyNhanVien_Client.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace QuanLyNhanVien_Client.Controllers
{
    public class HomeController : Controller
    {
        [ValidateSessionAttribute]
        [AllowAnonymous]
        public ActionResult Index()
        {
            NhanVienClient nhanVienClient = new NhanVienClient();

            IEnumerable<NhanVien> lstNhanVien = nhanVienClient.FindALL();

            GetTotalEmployee();

            GetTotalDepartment();

            GetTotalPosition();

            return View(lstNhanVien.ToList().Take(7));
        }

        public int GetTotalEmployee()
        {
            NhanVienClient nvClient = new NhanVienClient();
            IEnumerable<NhanVien> lstNhanVien = nvClient.FindALL();
            var result = lstNhanVien.Count();
            return ViewBag.totalEmployee = result;            
        }

        public int GetTotalDepartment()
        {
            PhongBanClient pbClient = new PhongBanClient();
            IEnumerable<PhongBan> lstPhongBan = pbClient.FindALL();
            var result = lstPhongBan.Count();
            return ViewBag.totalDepartment = result;
        }

        public int GetTotalPosition()
        {
            ChucVuClient cvClient = new ChucVuClient();
            IEnumerable<ChucVu> lstChucVu = cvClient.FindALL();
            var result = lstChucVu.Count();
            return ViewBag.totalPosition = result;
        }

        private void GetPhongBan()
        {
            PhongBanClient client = new PhongBanClient();
            IEnumerable<PhongBan> pb = client.FindALL();
            SelectList departmentList = new SelectList(pb, "MaPhongBan", "TenPhongBan");
            ViewBag.DepartmentList = departmentList;

        }

        public ActionResult Login()
        {
            return View("Login", "NhanVien_PhongBan");
        }

     
    }
}