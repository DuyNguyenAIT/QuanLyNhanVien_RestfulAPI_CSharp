using QuanLyNhanVien_WebAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using System.Data.Entity;
using System.Data;
using System.Net;
namespace QuanLyNhanVien_WebAPI.Controllers
{
    public class NhanVienController : ApiController
    {
        private QuanLyNhanVienEntities db = new QuanLyNhanVienEntities();

        public NhanVienController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        public IEnumerable<NHANVIEN> GetNhanVien()
        {
            return db.NHANVIENs.ToList();
        }

        [ResponseType(typeof(NHANVIEN))]
        public IHttpActionResult GetNhanVien(int id)
        {
            NHANVIEN nhanviens = db.NHANVIENs.Find(id);
            if (nhanviens == null)
            {
                return NotFound();
            }
            return Ok(nhanviens);
        }

        [ResponseType(typeof(NHANVIEN))]
        public IHttpActionResult PostNhanVien(NHANVIEN nhanvien)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.NHANVIENs.Add(nhanvien);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = nhanvien.MaNhanVien }, nhanvien);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult PutNhanVien(int id, NHANVIEN nhanvien)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(id!= nhanvien.MaNhanVien)
            {
                return NotFound();
            }
            db.Entry(nhanvien).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DBConcurrencyException)
            {

                if(!NhanVienExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        public IHttpActionResult DeleteNhanVien(int id)
        {
            NHANVIEN nhanvien = db.NHANVIENs.Find(id);
            if (nhanvien == null)
            {
                return NotFound();
            }
            db.NHANVIENs.Remove(nhanvien);
            db.SaveChanges();

            return Ok(nhanvien);
        }

        [ResponseType(typeof(NHANVIEN))]
        public IHttpActionResult SearchNhanVien(int id)
        {
            NHANVIEN nhanviens = db.NHANVIENs.Find(id);
            if (nhanviens == null)
            {
                return NotFound();
            }
            return Ok(nhanviens);
        }


        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        private bool NhanVienExists(int id)
        {
            return db.NHANVIENs.Count(x => x.MaNhanVien == id) > 0;
        }
    }
}