using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Database_semester_project.Controllers
{
    public class TasksController : Controller
    {
        private readonly Models.FactoryEntities db = new Models.FactoryEntities();
        // GET: Tasks
        public ActionResult Index()
        {
            return View(db.Tasks.ToList());
        }

        // GET: Tasks/Details/5
        public ActionResult Details(int id)
        {
            var task = (from t in db.Tasks
                        where t.Id == id
                        select t).First();
            return View(task);
        }

        public ActionResult Create(int productID = -1)
        {
            var model = new Models.Tasks();
            if (productID != -1)
            {
                model.ProductID = productID;
                model.ProductLocked = true;
            }
            return View(model);
        }

        // POST: Tasks/Create
        [HttpPost]
        public ActionResult Create(Models.Tasks task)
        {
            if (!ModelState.IsValid)
                return View(task);
            task.Finished = false;

            db.Tasks.Add(task);
            try
            {
                db.SaveChanges();
            }
            catch(Exception e)
            {
                return View(task);
            }
            if (task.ProductLocked)
                return RedirectToAction("../Products/Details", new { id = task.ProductID });
            return RedirectToAction("Index");
        }

        // GET: Tasks/Edit/5
        public ActionResult Edit(int id)
        {
            var task = (from t in db.Tasks
                        where t.Id == id
                        select t).First();
            return View(task);
        }

        // POST: Tasks/Edit/5
        [HttpPost]
        public ActionResult Edit(Models.Tasks task)
        {
            if (!ModelState.IsValid)
                return View(task);

            var originalTask = (from t in db.Tasks
                                where t.Id == task.Id
                                select t).First();
            task.Finished = true;

            var product = (from p in db.Products
                           where p.Id == task.ProductID
                           select p).First();
            product.Stored_amount += task.Produced_quantity;

            db.Entry(originalTask).CurrentValues.SetValues(task);
            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                ViewBag.Exception = e.Message;
                return View(task);
            }
            return RedirectToAction("Index");
        }

        // GET: Tasks/Delete/5
        public ActionResult Delete(int id)
        {
            var task = (from t in db.Tasks
                        where t.Id == id
                        select t).First();
            db.Tasks.Remove(task);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AssignEmployee(int taskID)
        {
            var task = (from t in db.Tasks
                        where t.Id == taskID
                        select t).First();
            return View(task);
        }

        [HttpPost]
        public ActionResult AssignEmployee(Models.Tasks task)
        {
            var originalTask = (from t in db.Tasks
                                where task.Id == t.Id
                                select t).First();

            foreach (var e in originalTask.Employees)
            {
                if (!task.Employees.Contains(e))
                    task.Employees.Add(e);
            }
            db.Entry(originalTask).CurrentValues.SetValues(task);
            try
            {
                db.SaveChanges();
            }
            catch(Exception e)
            {
                ViewBag.Exception = e.Message;
                return View(task);
            }
            return RedirectToAction("Index");
        }
    }
}
