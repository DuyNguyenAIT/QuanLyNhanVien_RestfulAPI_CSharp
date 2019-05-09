using QuanLyNhanVien_Client.Authorize;
using QuanLyNhanVien_Client.Filter;
using QuanLyNhanVien_Client.Models;
using QuanLyNhanVien_Client.ViewModel;
using System.Web.Mvc;

namespace QuanLyNhanVien_Client.Controllers
{
    [ValidateSessionAttribute]
    [AuthorizePosition(TenChucVu = "Adminstator")]
    public class ChucVuController : Controller
    {
        static int maChucVu = 0;

        public ActionResult Index()
        {        
            ChucVuClient client = new ChucVuClient();
            ViewBag.lstChucVu = client.FindALL();
            return View();
        }

        public ActionResult Details(int id)
        {
            ChucVuClient client = new ChucVuClient();
            ChucVuViewModel cvVM = new ChucVuViewModel();
            cvVM.chucvus = client.Find(id);
            return View("Details", cvVM);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public ActionResult Create(ChucVuViewModel cvVM)
        {

            try
            {
                if(ModelState.IsValid)
                {
                    ChucVuClient client = new ChucVuClient();
                    client.Create(cvVM.chucvus);
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
        public ActionResult Edit(int id)
        {
            maChucVu = id;
            ChucVuClient client = new ChucVuClient();
            ChucVuViewModel cvVM = new ChucVuViewModel();
            cvVM.chucvus = client.Find(id);
            return View("Edit", cvVM);
        }

        [HttpPost]
        public ActionResult Edit(ChucVuViewModel cvVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    cvVM.chucvus.MaChucVu = maChucVu;
                    ChucVuClient client = new ChucVuClient();
                    client.Edit(cvVM.chucvus);
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
            maChucVu = id;
            ChucVuClient client = new ChucVuClient();
            ChucVuViewModel cvVM = new ChucVuViewModel();
            cvVM.chucvus = client.Find(id);
            return View("Delete", cvVM);
        }

        [HttpPost]
        public ActionResult Delete(ChucVuViewModel cvVM)
        {
            cvVM.chucvus.MaChucVu = maChucVu;
            ChucVuClient client = new ChucVuClient();
            client.Delete(cvVM.chucvus.MaChucVu);
            return RedirectToAction("Index");
        }

        public ActionResult Login()
        {
            return View("Login","NhanVien_PhongBan");
        }
    }
}