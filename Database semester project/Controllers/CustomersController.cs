using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Database_semester_project.Controllers
{
    public class CustomersController : Controller
    {
        private readonly Models.FactoryEntities db = new Models.FactoryEntities();
        // GET: Customers
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int id)
        {
            var customer = (from c in db.Customers
                            where c.Id == id
                            select c).First();
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View(new Models.Customers());
        }

        // POST: Customers/Create
        [HttpPost]
        public ActionResult Create(Models.Customers customer)
        {
            if (!ModelState.IsValid)
                return View(customer);

            db.Customers.Add(customer);
            try
            {
                db.SaveChanges();
            }
            catch(Exception e)
            {
                return View(customer);
            }
            return RedirectToAction("Index");
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int id)
        {
            var customer = (from c in db.Customers
                            where c.Id == id
                            select c).First();
            return View(customer);
        }

        // POST: Customers/Edit/5
        [HttpPost]
        public ActionResult Edit(Models.Customers customer)
        {
            if (!ModelState.IsValid)
                return View(customer);

            var originalCustomer = (from c in db.Customers
                                    where c.Id == customer.Id
                                    select c).First();

            db.Entry(originalCustomer).CurrentValues.SetValues(customer);
            try
            {
                db.SaveChanges();
            }
            catch(Exception e)
            {
                return View(customer);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var customer = (from c in db.Customers
                            where c.Id == id
                            select c).First();
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
