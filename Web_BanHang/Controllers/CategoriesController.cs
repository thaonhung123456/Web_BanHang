using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_BanHang.Models;

namespace Web_BanHang.Controllers
{
    public class CategoriesController : Controller
    {
        // GET: Categories
        DBSportStoreEntities db = new DBSportStoreEntities();
        public PartialViewResult CategoryPartial()// Cái này là bài tí tí sau này nha
        {
            var cateList = db.Categories.ToList();
            return PartialView(cateList);
        }
        public ActionResult Index(string _name)
        {
            if (_name == null)
                return View(db.Categories.ToList());
            else
                return View(db.Categories.Where(s => s.IDCate.Contains(_name)).ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Category cate)
        {
            try
            {
                db.Categories.Add(cate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Error Craete New");
            }
        }

        public ActionResult Details(int id)
        {
            return View(db.Categories.Where(s => s.Id == id).FirstOrDefault());
        }

        public ActionResult Edit(int id)
        {
            return View(db.Categories.Where(s => s.Id == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(int id, Category cate)
        {
            db.Entry(cate).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            return View(db.Categories.Where(s => s.Id == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(int id, Category cate)
        {
            try
            {
                cate = db.Categories.Where(s => s.Id == id).FirstOrDefault();
                db.Categories.Remove(cate);
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