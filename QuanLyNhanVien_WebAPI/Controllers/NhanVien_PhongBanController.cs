using QuanLyNhanVien_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web;
using System.Data.Entity.Infrastructure;

namespace QuanLyNhanVien_WebAPI.Controllers
{
    public class NhanVien_PhongBanController : ApiController
    {
        private QuanLyNhanVienEntities db = new QuanLyNhanVienEntities();

        public NhanVien_PhongBanController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        public IEnumerable<NHANVIEN_PHONGBAN> GetNhanVien_PhongBan()
        {
            return db.NHANVIEN_PHONGBAN.ToList();
        }

        [ResponseType(typeof(NHANVIEN_PHONGBAN))]
        [HttpGet]
        public IHttpActionResult GetNhanVien_PhongBan(int id)
        {
            NHANVIEN_PHONGBAN nhanvien_phongbans = db.NHANVIEN_PHONGBAN.Where(x => x.MaNV_PB == id).FirstOrDefault();
            if(nhanvien_phongbans == null)
            {
                return NotFound();
            }
            return Ok(nhanvien_phongbans);
        }

        [ResponseType(typeof(NHANVIEN_PHONGBAN))]
        [HttpPost]
        public IHttpActionResult PostNhanVien_PhongBan(NHANVIEN_PHONGBAN nhanvien_phongban)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.NHANVIEN_PHONGBAN.Add(nhanvien_phongban);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = nhanvien_phongban.MaNV_PB }, nhanvien_phongban);
        }


        [ResponseType(typeof(void))]
        [HttpPut]
        public IHttpActionResult PutNhanVien_PhongBan(int id, NHANVIEN_PHONGBAN nhanvien_phongban)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != nhanvien_phongban.MaNV_PB)
            {
                return BadRequest();
            }
            else
            {
                var model = db.NHANVIEN_PHONGBAN.FirstOrDefault(x=>x.MaNV_PB == id);
                model.MaPhongBan = nhanvien_phongban.MaPhongBan;
                model.MaChucVu = nhanvien_phongban.MaChucVu;
                model.MatKhau = nhanvien_phongban.MatKhau;
                db.SaveChanges();

            }
            return StatusCode(HttpStatusCode.NoContent);

        }


        [ResponseType(typeof(NHANVIEN_PHONGBAN))]
        public IHttpActionResult DeleteNhanVien_PhongBan(int id)
        {
            NHANVIEN_PHONGBAN nhanvien_phongban = db.NHANVIEN_PHONGBAN.Where(x => x.MaNV_PB == id).FirstOrDefault();
            if (nhanvien_phongban == null)
            {
                return NotFound();
            }
            db.NHANVIEN_PHONGBAN.Remove(nhanvien_phongban);
            db.SaveChanges();

            return Ok(nhanvien_phongban);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
