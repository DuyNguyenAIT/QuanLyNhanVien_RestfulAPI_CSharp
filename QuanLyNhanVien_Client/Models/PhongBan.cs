using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace QuanLyNhanVien_Client.Models
{
    [Bind(Exclude = "MaPhongBan", Include = "TenPhongBan,SoDienThoai")]
    public class PhongBan
    {
        public int MaPhongBan { get; set; }

        [Required(AllowEmptyStrings =false, ErrorMessage = "Department name is required!!!")]
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "The special characters is not allowed!")]
        [Display(Name = "Department Name")]
        [StringLength(100,MinimumLength = 5)]
        public string TenPhongBan { get; set; }


        [Display(Name = "Phone Number")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Phone department is required!!!")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Invalid a department phone number")]
        public string SoDienThoai { get; set; }




    }
}