using QuanLyNhanVien_Client.Authorize;
using QuanLyNhanVien_Client.Models;
using QuanLyNhanVien_Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyNhanVien_Client.Controllers
{
    public class TaiKhoanController : Controller
    {        
        public ActionResult Index()
        {
            NhanVien_PhongBanClient nv_pbClient = new NhanVien_PhongBanClient();

            IEnumerable<NhanVien_PhongBan> lstNhanVien_PhongBan = nv_pbClient.FindALL();

            NhanVienClient nhanVienClient = new NhanVienClient();

            PhongBanClient phongBanClient = new PhongBanClient();

            ChucVuClient chucvVuClient = new ChucVuClient();

            IEnumerable<NhanVien> lstNhanVien = nhanVienClient.FindALL();

            IEnumerable<PhongBan> lstPhongBan = phongBanClient.FindALL();

            IEnumerable<ChucVu> lstChucVu = chucvVuClient.FindALL();

            var result = (from x in lstNhanVien_PhongBan
                          join y in lstNhanVien on x.MaNhanVien equals y.MaNhanVien
                          join z in lstPhongBan on x.MaPhongBan equals z.MaPhongBan
                          join t in lstChucVu on x.MaChucVu equals t.MaChucVu
                          select new NhanVien_PhongBan()
                          {
                              MaNhanVien = y.MaNhanVien,
                              MaNV_PB = x.MaNV_PB,
                              MaChucVu = x.MaChucVu,
                              HoTen = y.HoTen,
                              TenPhongBan = z.TenPhongBan,
                              TenTaiKhoan = x.TenTaiKhoan,
                              MatKhau = x.MatKhau,
                              TenChucVu = t.TenChucVu,
                          }).ToList();

            int maNhanVien = (int)Session["MaNhanVien"];

            var dataOfEmployee = result.Where(x => x.MaNhanVien == maNhanVien).FirstOrDefault();

            if(dataOfEmployee.MaChucVu == 3)
            {
                ViewData["AccountOfEmployee"] = dataOfEmployee;
            }
            else
            {
                ViewBag.lstTaiKhoan = result.OrderBy(x=>x.TenPhongBan);

            }
            return View();             
        }

        [HttpGet]
        public ActionResult ChangePassword(int id)
        {
            NhanVien_PhongBanClient nv_pbClient = new NhanVien_PhongBanClient();
            NhanVien_PhongBan nv_pb = new NhanVien_PhongBan();
            nv_pb = nv_pbClient.Find(id);
            nv_pb.MatKhau = string.Empty;
            return View(nv_pb);
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult ChangePassword(NhanVien_PhongBanViewModel nv_pbVM)
        {           
            try
            {
                
                if (ModelState.IsValid)
                {
                    NhanVien_PhongBanClient nv_pbClient = new NhanVien_PhongBanClient();

                    IEnumerable<NhanVien_PhongBan> lstNhanVien_PhongBan = nv_pbClient.FindALL();

                    //Lay mat khau hien tai trong database ra

                    var matKhauTrongDB = lstNhanVien_PhongBan.Where(x => x.MaNV_PB == nv_pbVM.MaNV_PB).FirstOrDefault().MatKhau;

                    //Neu nhu mat khau cu nguoi dung nhap bang voi mat khau co trong database thi cho phep doi
                    if (matKhauTrongDB == AES256Encryption.EncryptionLibrary.EncryptText(nv_pbVM.MatKhau))
                    {
                        var matKhauMoi = AES256Encryption.EncryptionLibrary.EncryptText(nv_pbVM.MatKhauMoi);

                        if (matKhauMoi == AES256Encryption.EncryptionLibrary.EncryptText(nv_pbVM.RepeatMatKhau))
                        {
                            NhanVien_PhongBanViewModel editNhanVien_PhongBanVM = new NhanVien_PhongBanViewModel();

                            editNhanVien_PhongBanVM.nhanvien_phongbans.TenTaiKhoan = nv_pbVM.TenTaiKhoan;

                            editNhanVien_PhongBanVM.nhanvien_phongbans.MatKhau = matKhauMoi;

                            editNhanVien_PhongBanVM.nhanvien_phongbans.MaNV_PB = nv_pbVM.MaNV_PB;

                            editNhanVien_PhongBanVM.nhanvien_phongbans.MaNhanVien = nv_pbVM.MaNhanVien;

                            editNhanVien_PhongBanVM.nhanvien_phongbans.MaPhongBan = nv_pbVM.MaPhongBan;

                            editNhanVien_PhongBanVM.nhanvien_phongbans.MaChucVu = nv_pbVM.MaChucVu;

                            nv_pbClient.Edit(editNhanVien_PhongBanVM.nhanvien_phongbans);

                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Mật khẩu mới không trùng. Xin vui lòng nhập lại!!");
                        }
                    } 
                    else
                    {
                        ModelState.AddModelError("", "Mật khẩu không đúng. Xin vui lòng nhập lại!!!");
                        return View();
                    }
                }
            }
            catch
            {
                return View();
            }
            return View();
        }
    }
}