using QuanLyNhanVien_Client.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace QuanLyNhanVien_Client.ViewModel
{
    public class PhongBanViewModel
    {
        public PhongBan phongbans { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Department name is required!!!")]
        [Display(Name = "Department Name")]
        [StringLength(100, MinimumLength = 5)]
        public string TenPhongBan { get; set; }

        public int MaPhongBan { get; set; }

        [Display(Name = "Employee Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Employee name is required!!!")]
        public string HoTen { get; set; }

        [Display(Name = "Sex")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Sex is required!!!")]
        public string GioiTinh { get; set; }

        [Display(Name = "Birth")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Birthday is required!!!")]
        public DateTime NgaySinh { get; set; }

        [Display(Name = "CMND")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "CMND is required!!!")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Invalid a only CMND")]
        public string CMND { get; set; }

        [Display(Name = "Nation")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Nation is required!!!")]
        public string QuocGia { get; set; }

        [Display(Name = "Current Address")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Current address is required!!!")]
        public string DiaChiHienTai { get; set; }

        [Display(Name = "Phone Number")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Phone number is required!!!")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Invalid a phone number")]
        public string SoDienThoai { get; set; }

        [Display(Name = "Email")]
        [RegularExpression(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "Invalid a email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required!!!")]
        public string Email { get; set; }

        [Display(Name = "Position Name")]
        public string TenChucVu { get; set; }

    }
 
}