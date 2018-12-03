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
            if (TempData["noAddress"] != null)
                ViewBag.Exception = TempData["noAddress"].ToString();
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

            var productPrice = (from p in db.Products
                                where p.Id == order.ProductID
                                select p.Sell_price).First();

            order.Price = productPrice * order.Product_quantity;

            var customer = (from c in db.Customers
                            where c.Id == order.CustomerID
                            select c).First();

            if (order.Price < 2000 && !customer.Regular_customer)
                order.Delivery_price = 120;
                

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
            if (!ModelState.IsValid)
                return View(order);

            var originalOrder = (from o in db.Orders
                                 where o.Id == order.Id
                                 select o).First();

            var productPrice = (from p in db.Products
                                where p.Id == order.ProductID
                                select p.Sell_price).First();

            order.Price = productPrice * order.Product_quantity;

            if (order.Price < 2000)
                order.Delivery_price = 120;

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
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("index");
        }

        public ActionResult Complete(int id)
        {
            var order = (from o in db.Orders
                         where o.Id == id
                         select o).First();

            var originalOrder = (from o in db.Orders
                                 where o.Id == order.Id
                                 select o).First();
            order.Completed = true;

            var product = (from p in db.Products
                           where p.Id == order.ProductID
                           select p).First();
            if (product.Stored_amount > order.Product_quantity)
                product.Stored_amount -= order.Product_quantity;
            else
            {
                ViewBag.Exception = "There is not enough product in storage to complete order";
                return RedirectToAction("index");
            }
            db.Entry(originalOrder).CurrentValues.SetValues(order);
            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                TempData["noAddress"] = e.InnerException.InnerException.Message;
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

    }
}
