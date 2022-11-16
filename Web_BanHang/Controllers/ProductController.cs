using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_BanHang.Models;
using PagedList;
using System.Net;
using System.Data.Entity;

namespace Web_BanHang.Controllers
{
    public class ProductController : Controller
    {
        DBSportStoreEntities db = new DBSportStoreEntities();
        // GET: Product
        // GET: AdminUser
        
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category1);
            return View(products.ToList());
        }       
        public ActionResult SearchOption(double min = double.MinValue, double max = double.MaxValue)
        {
            var list = db.Products.Where(p => (double)p.Price >= min && (double)p.Price <= max).ToList();
            return View(list);
        }
        
        public ActionResult SelectCate()
        {
            Category se_cate = new Category();
            se_cate.ListCate = db.Categories.ToList<Category>();
            return PartialView(se_cate);
        }
        public ActionResult Search(string _name)
        {
            if (_name == null)
                return View(db.Products.ToList());
            else
                return View(db.Products.Where(s => s.NamePro.Contains(_name)).ToList());
        }
        public ActionResult Index_Customers(string category, int? page, double min = double.MinValue, double max = double.MaxValue)
        {
            int pageSize = 8;
            int pageNum = (page ?? 1);
            if (category == null)
            {
                var productList = db.Products.OrderByDescending(x => x.NamePro);
                return View(productList.ToPagedList(pageNum, pageSize));
            }    
            else
            {
                var productList = db.Products.OrderByDescending(x => x.Category).Where(x => x.Category == category);
                return View(productList.ToPagedList(pageNum, pageSize));
            }
        }
        
        public ActionResult Create()
        {
            ViewBag.Category = new SelectList(db.Categories, "IDCate", "NameCate");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,NamePro,DecriptionPro,Category,Price,ImagePro")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Category = new SelectList(db.Categories, "IDCate", "NameCate", product.Category);
            return View(product);
        }

        

        public ActionResult Edit(int id)
        {
            return View(db.Products.Where(s => s.ProductID == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(int id, Product name)
        {
            db.Entry(name).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            return View(db.Products.Where(s => s.ProductID == id).FirstOrDefault());
        }

        public ActionResult DetailCus(int id)
        {

            return View(db.Products.Where(s => s.ProductID == id).FirstOrDefault());
        }
        public ActionResult Remove(int id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.Remove_CartItem(id);
            return RedirectToAction("Index", "Product");
        }
        public ActionResult Delete(int id)
        {
            return View(db.Products.Where(s => s.ProductID == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(int id, Product cate)
        {
            try
            {
                cate = db.Products.Where(s => s.ProductID == id).FirstOrDefault();
                db.Products.Remove(cate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("This data is using in other table, Error Delete!");
            }
        }
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Product product = db.Products.Find(id);
        //    if (product == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.Category = new SelectList(db.Categories, "IDCate", "NameCate", product.Category);
        //    return View(product);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ProductID,NamePro,DecriptionPro,Category,Price,ImagePro")] Product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(product).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.Category = new SelectList(db.Categories, "IDCate", "NameCate", product.Category);
        //    return View(product);
        //}
        //public ActionResult Create()
        //{
        //    List<Category> list = db.Categories.ToList();
        //    ViewBag.listCategory = new SelectList(list, "IDCate", "NameCate", "");
        //    Product pro = new Product();
        //    return View(pro);
        //}

        //[HttpPost]
        //public ActionResult Create(Product pro)
        //{
        //    List<Category> list = db.Categories.ToList();
        //    try
        //    {
        //        if (pro.UploadImage != null)
        //        {
        //            string filename = Path.GetFileNameWithoutExtension(pro.UploadImage.FileName);
        //            string extent = Path.GetExtension(pro.UploadImage.FileName);
        //            filename = filename + extent;
        //            pro.ImagePro = "~/Content/images/" + filename;
        //            pro.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), filename));
        //        }
        //        ViewBag.listCategory = new SelectList(list, "IDCate", "NameCate", "");
        //        db.Products.Add(pro);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}