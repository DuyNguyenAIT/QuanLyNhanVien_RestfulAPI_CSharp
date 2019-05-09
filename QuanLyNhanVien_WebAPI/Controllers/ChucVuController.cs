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
    public class ChucVuController : ApiController
    {
        private QuanLyNhanVienEntities db = new QuanLyNhanVienEntities();

        public ChucVuController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        public IEnumerable<CHUCVU> GetChucVu()
        {
            return db.CHUCVUs.ToList();
        }

        [ResponseType(typeof(CHUCVU))]
        public IHttpActionResult GetChucVu(int id)
        {
            CHUCVU chucvus = db.CHUCVUs.Find(id);
            if (chucvus == null)
            {
                return NotFound();
            }
            return Ok(chucvus);
        }

        [ResponseType(typeof(CHUCVU))]
        public IHttpActionResult PostChucVu(CHUCVU chucvu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.CHUCVUs.Add(chucvu);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = chucvu.MaChucVu }, chucvu);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult PutChucVu(int id, CHUCVU chucvu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != chucvu.MaChucVu)
            {
                return BadRequest();
            }
            db.Entry(chucvu).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChucVuExists(id))
                {
                    return NotFound();
                }
                else throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(CHUCVU))]
        public IHttpActionResult DeleteChucVu(int id)
        {
            CHUCVU chucvu = db.CHUCVUs.Find(id);
            if (chucvu == null)
            {
                return NotFound();
            }
            db.CHUCVUs.Remove(chucvu);
            db.SaveChanges();
            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ChucVuExists(int id)
        {
            return db.CHUCVUs.Count(x => x.MaChucVu == id) > 0;
        }
    }
}