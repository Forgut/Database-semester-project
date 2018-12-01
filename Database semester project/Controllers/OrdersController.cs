using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Database_semester_project.Controllers
{
    public class OrdersController : Controller
    {
        private readonly Models.FactoryEntities db = new Models.FactoryEntities();
        // GET: Orders
        public ActionResult Index()
        {
            return View(db.Orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int id)
        {
            var order = (from o in db.Orders
                         where o.Id == id
                         select o).First();
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            return View(new Models.Orders());
        }

        // POST: Orders/Create
        [HttpPost]
        public ActionResult Create(Models.Orders order)
        {
            if (!ModelState.IsValid)
                return View(order);

            db.Orders.Add(order);
            try
            {
                db.SaveChanges();
            }
            catch(Exception e)
            {
                return View(order);
            }
            return RedirectToAction("Index");
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int id)
        {
            var order = (from o in db.Orders
                         where o.Id == id
                         select o).First();
            return View(order);
        }

        // POST: Orders/Edit/5
        [HttpPost]
        public ActionResult Edit(Models.Orders order)
        {
            if (ModelState.IsValid)
                return View(order);

            var originalOrder = (from o in db.Orders
                                 where o.Id == order.Id
                                 select o).First();

            db.Entry(originalOrder).CurrentValues.SetValues(order);
            try
            {
                db.SaveChanges();
            }
            catch(Exception e)
            {
                return View(order);
            }
            return RedirectToAction("Index");
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int id)
        {
            var order = (from o in db.Orders
                         where o.Id == id
                         select o).First();
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost]
        public ActionResult Delete(Models.Orders order)
        {
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
