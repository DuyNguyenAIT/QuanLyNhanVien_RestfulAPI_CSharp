using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace QuanLyNhanVien_Client.Models
{
    [Bind(Exclude= "MaNhanVien", Include= "HoTen,GioiTinh,NgaySinh,CMND,QuocGia,TonGiao,DiaChiHienTai,SoDienThoai,Email,TinhTrangLamViec")]
    public class NhanVien
    {

        public NhanVien()
        {
            TinhTrangLamViec = true;
        }

        [Key]
        [Display(Name = "Employee ID")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Invalid a employee ID")]
        public int MaNhanVien { get; set; }

        [Display(Name = "Employee Name")]
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "The special characters is not allowed!")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Employee name is required!!!")]
        public string HoTen { get; set; }

        [Display(Name = "Sex")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Sex is required!!!")]
        public string GioiTinh { get; set; }

        [Display(Name = "Birth")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Birthday is required!!!")]
        public DateTime? NgaySinh { get; set; }

        [Display(Name = "CMND")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "CMND is required!!!")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Invalid a only CMND")]
        public string CMND { get; set; }

        [Display(Name = "Nation")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Nation is required!!!")]
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "The special characters is not allowed!")]
        public string QuocGia { get; set; }

        [Display(Name = "Religion")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Religion is required!!!")]
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "The special characters is not allowed!")]
        public string TonGiao { get; set; }

        [Display(Name = "Current Address")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Current address is required!!!")]
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "The special characters is not allowed!")]
        public string DiaChiHienTai { get; set; }

        [Display(Name = "Phone Number")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Phone number is required!!!")]
        [RegularExpression("^[0-9]*$",ErrorMessage = "Invalid a phone number")]
        public string SoDienThoai { get; set; }

        [Display(Name = "Email")]
        [RegularExpression(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "Invalid a email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required!!!")]
        public string Email { get; set; }

        [Display(Name = "Work Status")]
        [Required(AllowEmptyStrings = true, ErrorMessage = "Work status is required!!!")]
        public bool TinhTrangLamViec { get; set; }

        [Display(Name = "Department ID")]
        public int MaPhongBan { get; set; }

        [Display(Name = "Position ID")]
        public int MaChucVu { get; set; }

        [Display(Name = "Department Name")]
        public string TenPhongBan { get; set; }

        [Display(Name = "Position Name")]
        public string TenChucVu { get; set; }

        public int MaNV_PB { get; set; }

        public DateTime NgayTao { get; set; }

      
    }
}