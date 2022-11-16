using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web_BanHang.Models;

namespace Web_BanHang.Controllers
{
    public class OrderProController : Controller
    {  
        private DBSportStoreEntities db = new DBSportStoreEntities();
        // GET: OrderPro
        public ActionResult Index(string _name)
        {
            if (_name == null)
                return View(db.OrderProes.ToList());
            else
                return View(db.OrderProes.Where(s => s.NameCus.Contains(_name)).ToList());
        }       
        public ActionResult Details(int id)
        {
            return View(db.OrderProes.Where(s => s.IDCus == id).FirstOrDefault());
        }
        public ActionResult Edit(int id)
        {
            return View(db.OrderPro.Where(s => s.IDCus == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(int id,OrderPro cate)
        {
            db.Entry(cate).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            return View(db.OrderPro.Where(s => s.IDCus == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete( int id, Customer cate)
        {
            try
            {
                cate = db.OrderPro.Where(s => s.IDCus == id).FirstOrDefault();
                db.OrderPro.Remove(cate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("This data is using in other table, Error Delete!");
            }
        }
    }
}