using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnTapASP.Models;

namespace OnTapASP.Controllers
{
    public class NhanviensController : Controller
    {
        private Model1 db = new Model1();

        // GET: Nhanviens
        public ActionResult Index()
        {
            var nhanviens = db.Nhanviens.Include(n => n.Phongban);
            return View(nhanviens.ToList());
        }
        [HttpGet]
        public ActionResult Login()
        {
            var nhanviens = db.Nhanviens.Include(n => n.Phongban);
            return View(nhanviens.ToList());
        }
        [HttpPost]
        public ActionResult Login(int user, string pass)
        {
            var a = db.Nhanviens.Where(p => p.manv == user && p.matkhau == pass).FirstOrDefault();
            if (a != null)
            {
                Session["user"] = a.hotennv;
                return RedirectToAction("Index", "NhanViens");
            }
            else
            {
                ViewBag.err = "Sai tên đăng nhập hoặc mật khẩu";
                return View("Login");
            }
        }
        public ActionResult Logout()
        {
            Session.Remove("user");
            return RedirectToAction("Login", "NhanViens");
        }
        public ActionResult Menu2()
        {
            return PartialView(db.Phongbans.ToList());
        }
        [Route("danhmuc/{ma?}")]
        public ActionResult BYID(int ma)
        {
            return View(db.Nhanviens.Where(p=>p.maphong==ma).ToList());
        }
        // GET: Nhanviens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nhanvien nhanvien = db.Nhanviens.Find(id);
            if (nhanvien == null)
            {
                return HttpNotFound();
            }
            return View(nhanvien);
        }

        // GET: Nhanviens/Create
        public ActionResult Create()
        {
            ViewBag.maphong = new SelectList(db.Phongbans, "maphong", "tenphong");
            return View();
        }

        // POST: Nhanviens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create( Nhanvien emp)
        {
            try
            {
                db.Nhanviens.Add(emp);
                db.SaveChanges();
                return Json(new { a = true, JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex) { 
                return Json(new { a = false, error = ex.Message });
            }
        }

        // GET: Nhanviens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nhanvien nhanvien = db.Nhanviens.Find(id);
            if (nhanvien == null)
            {
                return HttpNotFound();
            }
            ViewBag.maphong = new SelectList(db.Phongbans, "maphong", "tenphong", nhanvien.maphong);
            return View(nhanvien);
        }

        // POST: Nhanviens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "manv,hotennv,tuoi,diachi,luongnv,maphong,matkhau")] Nhanvien nhanvien)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(nhanvien).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex) {
                    ViewBag.err = "Lỗi không thêm được!" + ex.Message;
                }
            }
            ViewBag.maphong = new SelectList(db.Phongbans, "maphong", "tenphong", nhanvien.maphong);
            return View(nhanvien);
        }

        // GET: Nhanviens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nhanvien nhanvien = db.Nhanviens.Find(id);
            if (nhanvien == null)
            {
                return HttpNotFound();
            }
            return View(nhanvien);
        }

        // POST: Nhanviens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Nhanvien nhanvien = db.Nhanviens.Find(id);
                db.Nhanviens.Remove(nhanvien);
                db.SaveChanges();
                TempData["DeleteSuccess"] = "Đã xóa thành công!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.err = "Lỗi không xóa được" + ex.Message;
                return View();
            }
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
