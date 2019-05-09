using QuanLyNhanVien_Client.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLyNhanVien_Client.ViewModel
{
    public class NhanVien_PhongBanViewModel
    {
        public NhanVien_PhongBan nhanvien_phongbans { get; set; }

        public NhanVien_PhongBanViewModel()
        {
            nhanvien_phongbans = new NhanVien_PhongBan();
        }

        [Key]
        public int MaNV_PB { get; set; }

        [Display(Name = "Employee ID")]
        public int MaNhanVien { get; set; }

        [Display(Name = "Department ID")]
        public int MaPhongBan { get; set; }

        [Display(Name = "Position ID")]
        public int MaChucVu { get; set; }

        public string HoTen { get; set; }

        public string TenPhongBan { get; set; }

        public string TenChucVu { get; set; }

        [Required(ErrorMessage = "Required enter a username")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Username must be minimum 2 characters")]
        [Display(Name = "Username")]
        public string TenTaiKhoan { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Required a password")]
        [MaxLength(30, ErrorMessage = "Password cannot be greater than 30 characters")]
        [StringLength(31, MinimumLength = 6, ErrorMessage = "Password must be minimum 7 charaters")]
        [Display(Name = "Password")]
        public string MatKhau { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Required a repeart password")]
        [MaxLength(30, ErrorMessage = "Password cannot be greater than 30 characters")]
        [StringLength(31, MinimumLength = 6, ErrorMessage = "Password must be minimum 7 charaters")]
        [Display(Name = "Repeat Password")]
        public string RepeatMatKhau { get; set; }

        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Required a repeart password")]
        [MaxLength(30, ErrorMessage = "Password cannot be greater than 30 characters")]
        [StringLength(31, MinimumLength = 6, ErrorMessage = "Password must be minimum 6 charaters")]
        [Display(Name = "New Password")]
        public string MatKhauMoi { get; set; }


        public bool RememberMe { get; set; }


    }
}