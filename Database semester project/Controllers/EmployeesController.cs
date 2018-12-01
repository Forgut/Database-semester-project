using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Database_semester_project.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly Models.FactoryEntities db = new Models.FactoryEntities();
        // GET: Employees
        public ActionResult Index()
        {
            return View(db.Employees.ToList());
        }

        // GET: Employees/Details/5
        public ActionResult Details(int id)
        {
            var employee = (from e in db.Employees
                            where e.Id == id
                            select e).First();
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View(new Models.Employees());
        }

        // POST: Employees/Create
        [HttpPost]
        public ActionResult Create(Models.Employees employee)
        {
            if (!ModelState.IsValid)
                return View(employee);

            db.Employees.Add(employee);
            try
            {
                db.SaveChanges();
            }
            catch(Exception e)
            {
                return View(employee);
            }
            return RedirectToAction("Index");
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int id)
        {
            var employee = (from e in db.Employees
                        where e.Id == id
                        select e).First();
            return View(employee);
        }

        // POST: Employees/Edit/5
        [HttpPost]
        public ActionResult Edit(Models.Employees employee)
        {
            if (!ModelState.IsValid)
                return View(employee);

            var originalEmployee = (from e in db.Employees
                                    where e.Id == employee.Id
                                    select e).First();
            db.Entry(originalEmployee).CurrentValues.SetValues(employee);
            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return View(employee);
            }
            return RedirectToAction("Index");
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int id)
        {
            var employee = (from e in db.Employees
                            where e.Id == id
                            select e).First();
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost]
        public ActionResult Delete(Models.Employees employee)
        {
            db.Employees.Remove(employee);
            try
            {
                db.SaveChanges();
            }
            catch(Exception e)
            {
                return View(employee.Id);
            }
            return RedirectToAction("Index");
            
        }
    }
}
