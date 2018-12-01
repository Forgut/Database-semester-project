using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Database_semester_project.Controllers
{
    public class ProductsController : Controller
    {
        private readonly Models.FactoryEntities db = new Models.FactoryEntities();
        // GET: Products
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int id)
        {
            var product = (from p in db.Products
                           where p.Id == id
                           select p).First();
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View(new Models.Products());
        }

        // POST: Products/Create
        [HttpPost]
        public ActionResult Create(Models.Products product)
        {
            if (!ModelState.IsValid)
                return View(product);

            db.Products.Add(product);
            try
            {
                db.SaveChanges();
            }
            catch(Exception e)
            {
                return View(product);
            }
            return RedirectToAction("Index");
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int id)
        {
            var product = (from p in db.Products
                           where p.Id == id
                           select p).First();
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        public ActionResult Edit(Models.Products product)
        {
            if (!ModelState.IsValid)
                return View(product);

            var originalProduct = (from p in db.Products
                                   where p.Id == product.Id
                                   select p).First();

            db.Entry(originalProduct).CurrentValues.SetValues(product);
            try
            {
                db.SaveChanges();
            }
            catch(Exception e)
            {
                return View(product);
            }
            return RedirectToAction("Index");
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int id)
        {
            var product = (from p in db.Products
                           where p.Id == id
                           select p).First();
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost]
        public ActionResult Delete(Models.Products product)
        {
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
