using PagedList;
using QuanLyNhanVien_Client.Authorize;
using QuanLyNhanVien_Client.Filter;
using QuanLyNhanVien_Client.Models;
using QuanLyNhanVien_Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace QuanLyNhanVien_Client.Controllers
{
    [ValidateSessionAttribute]
    public class NhanVienController : Controller
    {
        static int maNhanVien = 0;

        [HttpGet]
        public ActionResult Index(int? page)
        {

            int pageSize = 9;
            int pageNumber = (page ?? 1);

            NhanVienClient client = new NhanVienClient();

            NhanVien_PhongBanClient nv_pbClient = new NhanVien_PhongBanClient();

            IEnumerable<NhanVien_PhongBan> lstNhanVien_PhongBan = nv_pbClient.FindALL();

            //Lấy mã nhân viên khi nhân viên đó đăng nhập
            int maNhanVien = (int)Session["MaNhanVien"];

            var result = lstNhanVien_PhongBan.Where(x => x.MaNhanVien == maNhanVien).FirstOrDefault();

            if(result.MaChucVu == 3)
            {
                GetPhongBan();
                ViewData["OneEmployee"] = client.Find(maNhanVien);
                return View();
            }
            else
            {
                GetPhongBan();
                IEnumerable<NhanVien> lstNhanVien = client.FindALL().ToPagedList(pageNumber, pageSize);
                return View(lstNhanVien);
            }
 
        }

        [HttpPost]
        public ActionResult Index(int? page, int MaPhongBan)
        {
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            NhanVienClient nvClient = new NhanVienClient();
            PhongBanClient pvClient = new PhongBanClient();
            NhanVien_PhongBanClient nv_pbClient = new NhanVien_PhongBanClient();
            PhongBanClient pbClient = new PhongBanClient();

            if (MaPhongBan == 0)
            {
                IEnumerable<NhanVien> lstNhanVien = nvClient.FindALL();
                return View(lstNhanVien);
            }
            else
            {    
                IEnumerable<PhongBan> lstPhongBan = pbClient.FindALL();
                IEnumerable<NhanVien> lstNhanVien = nvClient.FindALL();
                IEnumerable<NhanVien_PhongBan> lstNhanVien_PB = nv_pbClient.FindALL();
                var getLstNV = (from x in lstNhanVien
                               join y in lstNhanVien_PB on x.MaNhanVien equals y.MaNhanVien
                               join t in lstPhongBan on y.MaPhongBan equals t.MaPhongBan
                               select new NhanVien()
                               {
                                   MaNhanVien = x.MaNhanVien,                                   
                                   HoTen = x.HoTen,
                                   GioiTinh = x.GioiTinh,
                                   NgaySinh = x.NgaySinh,
                                   CMND = x.CMND,
                                   QuocGia = x.QuocGia,
                                   SoDienThoai = x.SoDienThoai,
                                   Email = x.Email,
                                   MaPhongBan = t.MaPhongBan,
                               }).Where(x => x.MaPhongBan == MaPhongBan).ToList();

                IEnumerable<NhanVien> lstNhanVienTheoPhongBan = getLstNV.ToPagedList(pageNumber, pageSize);

                GetPhongBan();

                return View(lstNhanVienTheoPhongBan);
            }
            
        }
        
        [HttpGet]
        [AuthorizePosition(TenChucVu = "Adminstator")]
        public ActionResult Create()
        {
            GetCountryList();
            return View("Create");
        }

        [HttpPost]
        public ActionResult Create(NhanVienViewModel nvVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    NhanVienClient client = new NhanVienClient();
                    client.Create(nvVM.nhanviens);
                    return RedirectToAction("Index");
                }
            }
            catch
            {             
                return View();
            }
            GetCountryList();
            return View();
          
        }


        public ActionResult Details(int id)
        {
            NhanVienClient client = new NhanVienClient();
            NhanVienViewModel nvVM = new NhanVienViewModel();
            nvVM.nhanviens = client.Find(id);
            return View("Details", nvVM);
        }

        [HttpGet]
        [AuthorizePosition(TenChucVu = "Adminstator")]
        public ActionResult Edit(int id)
        {
            NhanVienClient nvClient = new NhanVienClient();
            NhanVien_PhongBanClient nv_pbClient = new NhanVien_PhongBanClient();
            ChucVuClient cvClient = new ChucVuClient();
            PhongBanClient pbClient = new PhongBanClient();
            IEnumerable<ChucVu> lstChucVu = cvClient.FindALL();
            IEnumerable<PhongBan> lstPhongBan = pbClient.FindALL();
            IEnumerable<NhanVien> lstNhanVien = nvClient.FindALL();
            IEnumerable<NhanVien_PhongBan> lstNhanVien_PB = nv_pbClient.FindALL();
            var querrAA = (from x in lstNhanVien
                           join y in lstNhanVien_PB on x.MaNhanVien equals y.MaNhanVien
                           join z in lstChucVu on y.MaChucVu equals z.MaChucVu
                           join t in lstPhongBan on y.MaPhongBan equals t.MaPhongBan
                           select new NhanVien_PhongBan()
                           {
                               MaNV_PB = y.MaNV_PB,
                               MaNhanVien = x.MaNhanVien,
                               MaPhongBan = y.MaPhongBan,
                               MaChucVu = z.MaChucVu,
                               HoTen = x.HoTen,
                               TenPhongBan = t.TenPhongBan,
                               TenChucVu = z.TenChucVu,
                               MatKhau = y.MatKhau,

                           }).Where(x => x.MaNhanVien == id).ToList();

            ViewBag.lstNhanVien_PhongBan = querrAA.ToList();

            return View("Edit");
        }

        [HttpGet]
        [ActionName("DepartmentEdit")]
        public ActionResult DepartmentEdit(int MaNV_PB, int MaNhanVien, int MaPhongBan, int MaChucVu)
        {  

            NhanVien_PhongBanViewModel nv_pbVM = new NhanVien_PhongBanViewModel();

            ChucVuClient cvClient = new ChucVuClient();
            NhanVienClient nvClient = new NhanVienClient();
            PhongBanClient pbClient = new PhongBanClient();
            NhanVien_PhongBanClient nv_pbClient = new NhanVien_PhongBanClient();
            nv_pbVM.nhanvien_phongbans.MaNV_PB = MaNV_PB;
            nv_pbVM.nhanvien_phongbans.MaNhanVien = MaNhanVien;
            nv_pbVM.nhanvien_phongbans.MaPhongBan = MaPhongBan;
            nv_pbVM.nhanvien_phongbans.MaChucVu = MaChucVu;

            IEnumerable<ChucVu> lstChucVu = cvClient.FindALL();
            IEnumerable<NhanVien> lstNhanVien = nvClient.FindALL();
            IEnumerable<PhongBan> lstPhongBan = pbClient.FindALL();

            var getTenChucVu = lstChucVu.Where(x => x.MaChucVu == MaChucVu).FirstOrDefault();
            nv_pbVM.nhanvien_phongbans.TenChucVu = getTenChucVu.TenChucVu;

            var getTenPhongBan = lstPhongBan.Where(x => x.MaPhongBan == MaPhongBan).FirstOrDefault();
            nv_pbVM.nhanvien_phongbans.TenPhongBan = getTenPhongBan.TenPhongBan;

            var getTenNhanVien = lstNhanVien.Where(x => x.MaNhanVien == MaNhanVien).FirstOrDefault();

            nv_pbVM.nhanvien_phongbans.HoTen = getTenNhanVien.HoTen;

            ViewBag.DepartmentList = new SelectList(lstPhongBan, "MaPhongBan", "TenPhongBan", nv_pbVM.MaPhongBan);

            ViewBag.PositionList = new SelectList(lstChucVu, "MaChucVu", "TenChucVu", nv_pbVM.MaChucVu);

            return View(nv_pbVM);
        }

        [HttpPost]
        [ActionName("DepartmentEdit")]
        [ValidateAntiForgeryToken()]
        [AuthorizePosition(TenChucVu = "Adminstator")]
        public ActionResult DepartmentEdit(NhanVien_PhongBanViewModel nv_pbVM)
        {
            NhanVien_PhongBanClient client = new NhanVien_PhongBanClient();

            NhanVien_PhongBanViewModel editNhanVienVM = new NhanVien_PhongBanViewModel();

            //lay ra danh sach cac nhan vien co trong phong ban
            var lstNhanVien_PhongBan = client.FindALL();

            var lstNhanVienOfPhongBan = lstNhanVien_PhongBan.Where(x => x.MaNhanVien == nv_pbVM.nhanvien_phongbans.MaNhanVien).ToList();

            foreach (var itemNV in lstNhanVienOfPhongBan)
            {
                if(itemNV.MaNhanVien == 0)
                {
                    return View();
                }
                if(itemNV.MaPhongBan == nv_pbVM.MaPhongBan && itemNV.MaChucVu == nv_pbVM.MaChucVu)
                {
                    GetPhongBan();
                    GetChucVu();
                    ModelState.AddModelError("", "Department of employee or position has been created!!");
                    return View();
                }
            }

            //Neu nhu khong trung thi cho phep chinh sua

            var matKhau = lstNhanVienOfPhongBan.FirstOrDefault(x => x.MaNhanVien == nv_pbVM.nhanvien_phongbans.MaNhanVien).MatKhau;

            editNhanVienVM.nhanvien_phongbans.MatKhau = matKhau;

            editNhanVienVM.nhanvien_phongbans.MaNhanVien = nv_pbVM.nhanvien_phongbans.MaNhanVien;

            editNhanVienVM.nhanvien_phongbans.MaNV_PB = nv_pbVM.nhanvien_phongbans.MaNV_PB;

            editNhanVienVM.nhanvien_phongbans.MaPhongBan = nv_pbVM.MaPhongBan;

            editNhanVienVM.nhanvien_phongbans.MaChucVu = nv_pbVM.MaChucVu;

            client.Edit(editNhanVienVM.nhanvien_phongbans);

            return RedirectToAction("Index");

            //}
        }

        [HttpGet]
        [ActionName("EmployeeEdit")]
        public ActionResult EmployeeEdit(int id)
        {
            maNhanVien = id;

            NhanVienClient nhanvienClient = new NhanVienClient();
            NhanVienViewModel nvVM = new NhanVienViewModel();
            nvVM.nhanviens = nhanvienClient.Find(id);
            return View(nvVM);            
        }

        [HttpPost]
        [ActionName("EmployeeEdit")]
        [ValidateAntiForgeryToken()]
        public ActionResult EmployeeEdit(NhanVienViewModel nvVM)
        {
            nvVM.nhanviens.MaNhanVien = maNhanVien;
            
            try
            {
                if (ModelState.IsValid)
                {
                    NhanVienClient nhanVienClient = new NhanVienClient();
                    nhanVienClient.Edit(nvVM.nhanviens);
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
        [AuthorizePosition(TenChucVu = "Adminstator")]
        public ActionResult Delete(int id)
        {
            maNhanVien = id;

            NhanVienClient nhanvienClient = new NhanVienClient();
            NhanVienViewModel nvVM = new NhanVienViewModel();
            nvVM.nhanviens = nhanvienClient.Find(id);
            return View(nvVM);            
        }

        [HttpPost]
        public ActionResult Delete(NhanVienViewModel nvVM)
        {

            nvVM.nhanviens.MaNhanVien = maNhanVien;

            NhanVien_PhongBanClient nv_pbClient = new NhanVien_PhongBanClient();

            NhanVienClient nhanvienClient = new NhanVienClient();

            IEnumerable<NhanVien_PhongBan> lstNhanVien_PhongBan = nv_pbClient.FindALL();

            IEnumerable<NhanVien_PhongBan> query = lstNhanVien_PhongBan.Where(x => x.MaNhanVien == nvVM.nhanviens.MaNhanVien).ToList();

            foreach (var item in query)
            {
                nv_pbClient.Delete(item.MaNV_PB);
                
            }

            nhanvienClient.Delete(nvVM.nhanviens.MaNhanVien);            

            return RedirectToAction("Index");
        }

        [HttpGet]
        [AuthorizePosition(TenChucVu = "Adminstator")]
        [ActionName("DeleteEmployeeInDepartment")]
        public ActionResult DeleteEmployeeInDepartment(int maNV_PB, int maNhanVien, int maPhongBan, int maChucVu)
        {
            NhanVien_PhongBanClient nv_pbClient = new NhanVien_PhongBanClient();
            ChucVuClient cvClient = new ChucVuClient();
            NhanVienClient nvClient = new NhanVienClient();
            PhongBanClient pbClient = new PhongBanClient();

            IEnumerable<ChucVu> lstChucVu = cvClient.FindALL();
            IEnumerable<NhanVien> lstNhanVien = nvClient.FindALL();
            IEnumerable<PhongBan> lstPhongBan = pbClient.FindALL();
            IEnumerable<NhanVien_PhongBan> lstNhanVien_PB = nv_pbClient.FindALL();

            var querrAA = (from x in lstNhanVien
                           join y in lstNhanVien_PB on x.MaNhanVien equals y.MaNhanVien
                           join z in lstChucVu on y.MaChucVu equals z.MaChucVu
                           join t in lstPhongBan on y.MaPhongBan equals t.MaPhongBan
                           select new NhanVien_PhongBan()
                           {
                               MaNV_PB = y.MaNV_PB,
                               MaNhanVien = x.MaNhanVien,
                               MaPhongBan = t.MaPhongBan,
                               MaChucVu = z.MaChucVu,
                               HoTen = x.HoTen,
                               TenPhongBan = t.TenPhongBan,
                               TenChucVu = z.TenChucVu,
                           }).ToList();

            ViewBag.lstNhanVien_PhongBan = querrAA.Where(x => x.MaNhanVien == maNhanVien && x.MaPhongBan == maPhongBan && x.MaChucVu == maChucVu).FirstOrDefault();

            NhanVien_PhongBanViewModel nv_pbViewModel = new NhanVien_PhongBanViewModel();

            nv_pbViewModel.nhanvien_phongbans = ViewBag.lstNhanVien_PhongBan;

            return View(nv_pbViewModel);
        }

        [HttpPost]
        [ActionName("DeleteEmployeeInDepartment")]
        public ActionResult DeleteEmployeeInDepartment(NhanVien_PhongBanViewModel nv_pbVM)
        {
            NhanVien_PhongBanClient client = new NhanVien_PhongBanClient();

            client.Delete(nv_pbVM.nhanvien_phongbans.MaNV_PB);

            return RedirectToAction("Index");
        }

        public ActionResult Login()
        {
            return View("Login", "NhanVien_PhongBan");
        }


        private void GetPhongBan()
        {
            PhongBanClient client = new PhongBanClient();
            IEnumerable<PhongBan> pb = client.FindALL();
            SelectList departmentList = new SelectList(pb, "MaPhongBan", "TenPhongBan");
            ViewBag.DepartmentList = departmentList;

        }

        private void GetChucVu()
        {
            ChucVuClient client = new ChucVuClient();
            IEnumerable<ChucVu> cv = client.FindALL();
            SelectList positionList = new SelectList(cv, "MaChucVu", "TenChucVu");
            ViewBag.PositionList = positionList;

        }

        private void GetCountryList()
        {
            List<string> CountryList = new List<string>();
            CultureInfo[] CInfoList = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            foreach (CultureInfo CInfo in CInfoList)
            {
                RegionInfo R = new RegionInfo(CInfo.LCID);
                if (!(CountryList.Contains(R.EnglishName)))
                {
                    CountryList.Add(R.EnglishName);
                }
            }

            CountryList.Sort();
            ViewBag.CountryList = CountryList;
        }

    }
}