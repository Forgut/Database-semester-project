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

        // GET: Tasks/Create
        public ActionResult Create()
        {
            return View(new Models.Tasks());
        }

        // POST: Tasks/Create
        [HttpPost]
        public ActionResult Create(Models.Tasks task)
        {
            if (!ModelState.IsValid)
                return View(task);

            db.Tasks.Add(task);
            try
            {
                db.SaveChanges();
            }
            catch(Exception e)
            {
                return View(task);
            }
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

            db.Entry(originalTask).CurrentValues.SetValues(task);
            try
            {
                db.SaveChanges();
            }
            catch(Exception e)
            {
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
            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost]
        public ActionResult Delete(Models.Tasks task)
        {
            db.Tasks.Remove(task);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
