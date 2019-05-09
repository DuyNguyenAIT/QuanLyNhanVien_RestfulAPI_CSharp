using QuanLyNhanVien_Client.AES256Encryption;
using QuanLyNhanVien_Client.Authorize;
using QuanLyNhanVien_Client.Filter;
using QuanLyNhanVien_Client.Models;
using QuanLyNhanVien_Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace QuanLyNhanVien_Client.Controllers
{
    public class NhanVien_PhongBanController : Controller
    {
        private void GetNhanVien()
        {
            NhanVienClient client = new NhanVienClient();
            IEnumerable<NhanVien> nv = client.FindALL();
            SelectList employeeList = new SelectList(nv, "MaNhanVien", "HoTen");
            ViewBag.EmployeeList = employeeList;

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

        [HttpGet]
        [AuthorizePosition(TenChucVu = "Adminstator")]
        public ActionResult Create()
        {
            GetNhanVien();
            GetPhongBan();
            GetChucVu();
            return View();
        }

        [HttpPost]
        [AuthorizePosition(TenChucVu = "Adminstator")]
        public ActionResult Create(NhanVien_PhongBan nv_pb)
        {
            NhanVien_PhongBanViewModel nv_pbVM = new NhanVien_PhongBanViewModel();

            NhanVien_PhongBanClient client = new NhanVien_PhongBanClient();

            //Lay toan bo du lieu nhan vien da duoc them vao phong ban
            IEnumerable<NhanVien_PhongBan> lstNhanVien_PhongBan = client.FindALL();

            //Lay ma nhan vien hien tai co trong phong ban
            var getMaNhanVien = lstNhanVien_PhongBan.Where(x => x.MaNhanVien == nv_pb.MaNhanVien).FirstOrDefault();

            nv_pbVM.nhanvien_phongbans.MatKhau = AES256Encryption.EncryptionLibrary.EncryptText(nv_pb.MatKhau);

            nv_pbVM.nhanvien_phongbans.RepeatMatKhau = AES256Encryption.EncryptionLibrary.EncryptText(nv_pb.RepeatMatKhau);

            //Neu nhu chua co du lieu thi them moi vao
            if (getMaNhanVien == null)
            {

                nv_pbVM.nhanvien_phongbans.TenTaiKhoan = nv_pb.TenTaiKhoan;

                nv_pbVM.nhanvien_phongbans.MaNhanVien = nv_pb.MaNhanVien;

                nv_pbVM.nhanvien_phongbans.MaPhongBan = nv_pb.MaPhongBan;

                nv_pbVM.nhanvien_phongbans.MaChucVu = nv_pb.MaChucVu;


                client.Create(nv_pbVM.nhanvien_phongbans);

                return RedirectToAction("Index", "NhanVien");
            }

            //neu nhu da ton tai ma nhan vien trong csdl
            else
            {
                //Lay ma phong ban hien tai cua nhan vien do ra
                var getMaPhongBan = lstNhanVien_PhongBan.Where(x => x.MaNhanVien == nv_pb.MaNhanVien).ToList();

                //Duyet het xem co ma phong ban nao trung khong
                foreach (var item in getMaPhongBan)
                {
                    //neu nhu trung thi tra lai ve view
                    if (nv_pb.MaPhongBan == item.MaPhongBan)
                    {
                        GetNhanVien();
                        GetPhongBan();
                        GetChucVu();
                        ModelState.AddModelError("", "Your department you choose is the same!!!");
                        return View();
                    }
                }

                if (ValidateUsername(nv_pb))
                {
                    ModelState.Clear();
                    ModelState.AddModelError("", "Username of employee is already registered");
                    GetNhanVien();
                    GetPhongBan();
                    GetChucVu();
                    return View("Create");
                }

                if (string.Compare(nv_pbVM.nhanvien_phongbans.MatKhau, nv_pbVM.nhanvien_phongbans.RepeatMatKhau) == 0)
                {
                    //Khong trung thi add moi vao

                    nv_pbVM.nhanvien_phongbans.TenTaiKhoan = nv_pb.TenTaiKhoan;

                    nv_pbVM.nhanvien_phongbans.MaNhanVien = nv_pb.MaNhanVien;

                    nv_pbVM.nhanvien_phongbans.MaPhongBan = nv_pb.MaPhongBan;

                    nv_pbVM.nhanvien_phongbans.MaChucVu = nv_pb.MaChucVu;

                    client.Create(nv_pbVM.nhanvien_phongbans);

                    ModelState.Clear();

                    return RedirectToAction("Index", "NhanVien");
                }

                return View();
            }

        }


        [HttpGet]
        public ActionResult Login()
        {
            NhanVien_PhongBan nv_pb = checkCookie();

            return View(new NhanVien_PhongBanViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Login(NhanVien_PhongBanViewModel nv_pbVM, FormCollection frmLogin)
        {
            try
            {
                NhanVien_PhongBanClient nv_pbClient = new NhanVien_PhongBanClient();

                IEnumerable<NhanVien_PhongBan> lstNV_PB = nv_pbClient.FindALL();

                //Kiem tra tai khoan cua nhan vien theo ten tai khoan nhan vien nhap
                var login = lstNV_PB.Where(x => x.TenTaiKhoan == nv_pbVM.nhanvien_phongbans.TenTaiKhoan).FirstOrDefault();

                nv_pbVM.nhanvien_phongbans.MaNhanVien = login.MaNhanVien;


                if (login != null)
                {
                    if (string.IsNullOrEmpty(nv_pbVM.nhanvien_phongbans.TenTaiKhoan) && string.IsNullOrEmpty(nv_pbVM.nhanvien_phongbans.MatKhau))
                    {
                        ModelState.AddModelError("", "Enter username and password");
                    }
                    else if (string.IsNullOrEmpty(nv_pbVM.nhanvien_phongbans.TenTaiKhoan))
                    {
                        ModelState.AddModelError("", "Enter username");
                    }
                    else if (string.IsNullOrEmpty(nv_pbVM.nhanvien_phongbans.MatKhau))
                    {
                        ModelState.AddModelError("", "Enter password");

                    }
                    else if (login.TenTaiKhoan != nv_pbVM.nhanvien_phongbans.TenTaiKhoan && login.MatKhau == AES256Encryption.EncryptionLibrary.EncryptText(nv_pbVM.nhanvien_phongbans.MatKhau))
                    {
                        ModelState.AddModelError("", "Error username Please enter again!!");
                    }
                    else if (login.TenTaiKhoan != nv_pbVM.nhanvien_phongbans.TenTaiKhoan || login.MatKhau != AES256Encryption.EncryptionLibrary.EncryptText(nv_pbVM.nhanvien_phongbans.MatKhau))
                    {
                        ModelState.Clear();
                        ModelState.AddModelError("", "Error username or error password!!");
                    }
                    else
                    {
                        nv_pbVM.nhanvien_phongbans.MatKhau = EncryptionLibrary.EncryptText(nv_pbVM.nhanvien_phongbans.MatKhau);

                        if (ValidateRegisterEmployee(nv_pbVM.nhanvien_phongbans))
                        {
                            var accountID = GetLoggedUserID(nv_pbVM.nhanvien_phongbans);

                            var userName = GetUserNameWhenLoggin(nv_pbVM.nhanvien_phongbans);

                            Session["MaNhanVien"] = accountID;                          

                            Session["HoTen"] = userName;

                            Session["EmployeeCurrent"] = login;

                            var a = frmLogin["remember-me"];

                            if (a != null)
                            {
                                HttpCookie ckTenTaiKhoan = new HttpCookie("TenTaiKhoan");

                                ckTenTaiKhoan.Expires = DateTime.Now.AddSeconds(3600);

                                ckTenTaiKhoan.Value = nv_pbVM.nhanvien_phongbans.TenTaiKhoan;

                                Response.Cookies.Add(ckTenTaiKhoan);

                                HttpCookie ckMatKhau = new HttpCookie("MatKhau");

                                ckMatKhau.Expires = DateTime.Now.AddSeconds(3600);

                                ckMatKhau.Value = nv_pbVM.nhanvien_phongbans.MatKhau;

                                Response.Cookies.Add(ckMatKhau);

                            }


                            return RedirectToAction("Index", "Home");                          
 
                        }
                        else
                        {
                            ModelState.AddModelError("", "User is already registered");
                            return View("Create");
                        }

                    }
                }

                return View("Login");
            }
            catch (Exception)
            {

                throw;
            }
        } 

        public ActionResult Logout()
        {            
            Session.Abandon();
            if(Response.Cookies["TenTaiKhoan"] != null)
            {
                HttpCookie tenTaiKhoan = new HttpCookie("TenTaiKhoan");
                tenTaiKhoan.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(tenTaiKhoan);
            }
            if (Response.Cookies["MatKhau"] != null)
            {
                HttpCookie tenTaiKhoan = new HttpCookie("MatKhau");
                tenTaiKhoan.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(tenTaiKhoan);
            }

            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "NhanVien_PhongBan");
        }


        private int GetLoggedUserID(NhanVien_PhongBan nv_pb)
        {
            NhanVien_PhongBanClient nv_pbClient = new NhanVien_PhongBanClient();
            IEnumerable<NhanVien_PhongBan> lstNV_PB = nv_pbClient.FindALL();

            var useraccount = (from x in lstNV_PB
                               where x.TenTaiKhoan == nv_pb.TenTaiKhoan &&
                               x.MatKhau == nv_pb.MatKhau
                               select x.MaNhanVien).FirstOrDefault();

            return useraccount;
        }

        private bool ValidateRegisterEmployee(NhanVien_PhongBan nv_pb)
        {
            NhanVien_PhongBanClient nv_pbClient = new NhanVien_PhongBanClient();
            IEnumerable<NhanVien_PhongBan> lstNV_PB = nv_pbClient.FindALL();

            var useraccount = (from x in lstNV_PB
                               where x.TenTaiKhoan == nv_pb.TenTaiKhoan &&
                               x.MatKhau == nv_pb.MatKhau
                               select x.MaNhanVien).Count();

            if (useraccount > 0) return true;
            else return false;
        }

        private string GetUserNameWhenLoggin(NhanVien_PhongBan nv_pb)
        {
            NhanVienClient nhanVienclient = new NhanVienClient();

            IEnumerable<NhanVien> lstNhanVien = nhanVienclient.FindALL();

            var useraccount = (from x in lstNhanVien
                               where x.MaNhanVien == nv_pb.MaNhanVien
                               select x.HoTen).FirstOrDefault();

            return useraccount;
        }

        private bool ValidateUsername(NhanVien_PhongBan nv_pb)
        {
            NhanVien_PhongBanClient nv_pbClient = new NhanVien_PhongBanClient();

            IEnumerable<NhanVien_PhongBan> lstNV_PB = nv_pbClient.FindALL();

            foreach (var item in lstNV_PB)
            {
                if (item.TenTaiKhoan == nv_pb.TenTaiKhoan)
                {
                    return true;
                }

            }

            return false;
        }

        private NhanVien_PhongBan checkCookie()
        {
            NhanVien_PhongBan nv_pb = null;
            string tenTaiKhoan = string.Empty, matKhau = string.Empty;
            if(Response.Cookies["TenTaiKhoan"] != null)
            {
                tenTaiKhoan = Response.Cookies["TenTaiKhoan"].Value;
            }
            if(Response.Cookies["MatKhau"] != null)
            {
                matKhau = Response.Cookies["MatKhau"].Value;
            }
            if(!String.IsNullOrEmpty(tenTaiKhoan) && !String.IsNullOrEmpty(matKhau))
            {
                nv_pb = new NhanVien_PhongBan { TenTaiKhoan = tenTaiKhoan, MatKhau = matKhau };               
            }
            return nv_pb;
        }

        public void OnWindowClosing()
        {
            Session.Abandon();
        }

    }
}