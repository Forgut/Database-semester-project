using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Database_semester_project.Controllers
{
    public class IngredientsController : Controller
    {
        private Models.FactoryEntities db = new Models.FactoryEntities();
        // GET: Ingredients
        public ActionResult Index()
        {
            return View(db.Ingredients.ToList());
        }

        // GET: Ingredients/Details/5
        public ActionResult Details(int id)
        {
            var ingredient = (from i in db.Ingredients
                              where i.Id == id
                              select i).First();
            return View(ingredient);
        }

        // GET: Ingredients/Create
        public ActionResult Create()
        {
            return View(new Models.Ingredients());
        }

        // POST: Ingredients/Create
        [HttpPost]
        public ActionResult Create(Models.Ingredients ingredient)
        {
            if (!ModelState.IsValid)
                return View(ingredient);

            db.Ingredients.Add(ingredient);
            try
            {
                db.SaveChanges();
            }
            catch(Exception e)
            {
                return View(ingredient);
            }
            return RedirectToAction("Index");
        }

        // GET: Ingredients/Edit/5
        public ActionResult Edit(int id)
        {
            var ingredient = (from i in db.Ingredients
                              where i.Id == id
                              select i).First();
            return View(ingredient);
        }

        // POST: Ingredients/Edit/5
        [HttpPost]
        public ActionResult Edit(Models.Ingredients ingredient)
        {
            if (!ModelState.IsValid)
                return View(ingredient);

            var originalIngredient = (from i in db.Ingredients
                                      where i.Id == ingredient.Id
                                      select i).First();

            db.Entry(originalIngredient).CurrentValues.SetValues(ingredient);
            try
            {
                db.SaveChanges();
            }
            catch(Exception e)
            {
                return View(ingredient);
            }
            return RedirectToAction("Index");
        }

        // GET: Ingredients/Delete/5
        public ActionResult Delete(int id)
        {
                var ingredient = (from i in db.Ingredients
                                  where i.Id == id
                                  select i).First();
                db.Ingredients.Remove(ingredient);
            try
            {
                db.SaveChanges();
            }
            catch(Exception e)
            {
                return View(ingredient);
            }
            return RedirectToAction("Index");
        }
    }
}
