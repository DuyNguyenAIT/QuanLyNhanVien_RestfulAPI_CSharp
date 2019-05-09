using QuanLyNhanVien_Client.Authorize;
using QuanLyNhanVien_Client.Filter;
using QuanLyNhanVien_Client.Models;
using QuanLyNhanVien_Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
namespace QuanLyNhanVien_Client.Controllers
{
    [ValidateSessionAttribute]
    [AuthorizePosition(TenChucVu = "Adminstator")]
    public class PhongBanController : Controller
    {
        static int maPhongBan = 0;

        [AuthorizePosition(TenChucVu = "Adminstator")]
        public ActionResult Index()
        {
            PhongBanClient phongBanClient = new PhongBanClient();
            ViewBag.lstPhongBans = phongBanClient.FindALL();
            return View();
        }

        [HttpGet]        
        public ActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Create(PhongBan phongBan)
        {
            try
            {    
                if (ModelState.IsValid)
                {
                    PhongBanClient phongBanClient = new PhongBanClient();
                    phongBanClient.Create(phongBan);
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
            return View();         
        }

        public ActionResult Details(int id)
        {

            PhongBanClient phongBanClient = new PhongBanClient();

            IEnumerable<PhongBan> lstPhongBan = phongBanClient.FindALL();

            NhanVienClient nhanVienClient = new NhanVienClient();

            //Danh sách nhân viên
            IEnumerable<NhanVien> lstNhanVien = nhanVienClient.FindALL();

            NhanVien_PhongBanClient nhanvien_phongBanClient = new NhanVien_PhongBanClient();

            IEnumerable<NhanVien_PhongBan> lstNhanVien_PhongBan = nhanvien_phongBanClient.FindALL();

            ChucVuClient chucVuClient = new ChucVuClient();

            IEnumerable<ChucVu> lstChucVu = chucVuClient.FindALL();

            var result = (from x in lstPhongBan
                          join y in lstNhanVien_PhongBan on x.MaPhongBan equals y.MaPhongBan
                          join z in lstNhanVien on y.MaNhanVien equals z.MaNhanVien
                          join t in lstChucVu on y.MaChucVu equals t.MaChucVu
                          select new NhanVien()
                          {
                              MaNhanVien = z.MaNhanVien,
                              HoTen = z.HoTen,
                              GioiTinh = z.GioiTinh,
                              NgaySinh = z.NgaySinh,
                              CMND = z.CMND,
                              QuocGia = z.QuocGia,
                              TonGiao = z.TonGiao,
                              DiaChiHienTai = z.DiaChiHienTai,
                              SoDienThoai = z.SoDienThoai,
                              Email = z.Email,
                              MaPhongBan = x.MaPhongBan,
                              TenPhongBan = x.TenPhongBan,
                              TenChucVu = t.TenChucVu,
                          }).ToList();

            ViewBag.lstNhanVien_PB = result.Where(x => x.MaPhongBan == id).ToList();

            PhongBanViewModel pbVM = new PhongBanViewModel();

            pbVM.phongbans = phongBanClient.Find(id);            
            
            return View(pbVM);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            PhongBanClient phongBanClient = new PhongBanClient();
            PhongBan phongBan = new PhongBan();
            phongBan = phongBanClient.Find(id);
            maPhongBan = id;
            return View("Edit", phongBan);
        }

        [HttpPost]
        public ActionResult Edit(PhongBan phongBan)
        {
            phongBan.MaPhongBan = maPhongBan;
            try
            {
                if(ModelState.IsValid)
                {
                    PhongBanClient phongBanClient = new PhongBanClient();
                    phongBanClient.Edit(phongBan);
                    return RedirectToAction("Index");
                }               
            }
            catch
            {
                return View();
            }
            return View();
            
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            PhongBanClient phongBanClient = new PhongBanClient();
            PhongBan phongBan = new PhongBan();
            phongBan = phongBanClient.Find(id);
            maPhongBan = id;
            return View("Delete", phongBan);
        }

        [HttpPost]
        public ActionResult Delete(PhongBan phongBan)
        {
            phongBan.MaPhongBan = maPhongBan;
            PhongBanClient phongBanClient = new PhongBanClient();
            phongBanClient.Delete(phongBan.MaPhongBan);
            return RedirectToAction("Index");
        }

        public ActionResult Login()
        {
            return View("Login", "NhanVien_PhongBan");
        }
    }
}