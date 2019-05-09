using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLyNhanVien_Client.Models
{
    public class NhanVien_PhongBan
    {

        [Key]
        public int MaNV_PB { get; set; }
        
        [Display(Name = "Employee ID")]
        public int MaNhanVien { get; set; }

        [Display(Name = "Department ID")]
        public int MaPhongBan { get; set; }

        [Display(Name = "Position ID")]
        public int MaChucVu { get; set; }

        [Display(Name = "Employee Name")]
        public string HoTen { get; set; }

        [Display(Name = "Department Name")]
        public string TenPhongBan { get; set; }

        [Display(Name = "Position Name")]
        public string TenChucVu { get; set; }

        [Required(ErrorMessage = "Required enter a username")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Username must be minimum 2 characters")]
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""\s]+$", ErrorMessage = "The special characters is not allowed!")]
        [Display(Name = "Username")]
        public string TenTaiKhoan { get; set; }

        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings =false,ErrorMessage = "Required a password")]
        [MaxLength(30, ErrorMessage = "Password cannot be greater than 30 characters")]
        [StringLength(31, MinimumLength = 6, ErrorMessage = "Password must be minimum 6 charaters")]
        [Display(Name = "Password")]
        public string MatKhau { get; set; }

        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings =false,ErrorMessage = "Required a repeart password")]
        [MaxLength(30, ErrorMessage = "Password cannot be greater than 30 characters")]
        [StringLength(31, MinimumLength = 6, ErrorMessage = "Password must be minimum 6 charaters")]
        [Display(Name = "Repeat Password")]        
        public string RepeatMatKhau { get; set; }

        public bool RememberMe { get; set; }

        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Required a repeart password")]
        [MaxLength(30, ErrorMessage = "Password cannot be greater than 30 characters")]
        [StringLength(31, MinimumLength = 6, ErrorMessage = "Password must be minimum 6 charaters")]
        [Display(Name = "New Password")]
        public string MatKhauMoi { get; set; }
    }
}