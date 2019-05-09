using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace QuanLyNhanVien_Client.Models
{
    [Bind(Exclude= "MaChucVu",Include = "TenChucVu")]
    public class ChucVu
    {
        [Display(Name = "ID")]
        public int MaChucVu { get; set; }

        [Required(AllowEmptyStrings =false, ErrorMessage = "Position name is required!!!")]
        [Display(Name = "Position Name")]
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$",ErrorMessage = "The special characters is not allowed!")]
        public string TenChucVu { get; set; }
    }
}