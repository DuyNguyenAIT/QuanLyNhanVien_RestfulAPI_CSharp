using QuanLyNhanVien_WebAPI.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace QuanLyNhanVien_WebAPI.Controllers
{
    public class PhongBanController : ApiController
    {
        private QuanLyNhanVienEntities db = new QuanLyNhanVienEntities();

        public PhongBanController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        public IEnumerable<PHONGBAN> GetPhongBan()
        {
            return db.PHONGBANs.ToList();
        }

        [ResponseType(typeof(PHONGBAN))]
        public IHttpActionResult GetPhongBan(int id)
        {
            PHONGBAN phongbans = db.PHONGBANs.Find(id);
            if (phongbans == null)
            {
                return NotFound();
            }
            return Ok(phongbans);
        }

        [ResponseType(typeof(PHONGBAN))]
        public IHttpActionResult PostPhongBan(PHONGBAN phongban)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.PHONGBANs.Add(phongban);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = phongban.MaPhongBan }, phongban);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult PutPhongBan(int id, PHONGBAN phongban)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != phongban.MaPhongBan)
            {
                return BadRequest();
            }
            db.Entry(phongban).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                var customerExists = db.PHONGBANs.FirstOrDefault(x => x.MaPhongBan == id);
                if (!PhongBanExists(id))
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

        [ResponseType(typeof(PHONGBAN))]
        public IHttpActionResult DeletePhongBan(int id)
        {
            PHONGBAN phongban = db.PHONGBANs.Find(id);
            if (phongban == null)
            {
                return NotFound();
            }
            db.PHONGBANs.Remove(phongban);
            db.SaveChanges();

            return Ok(phongban);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PhongBanExists(int id)
        {
            return db.PHONGBANs.Count(x => x.MaPhongBan == id) > 0;
        }
    }
}