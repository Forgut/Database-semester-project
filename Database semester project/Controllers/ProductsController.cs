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
            if(TempData["errProduct"] != null)
                ViewBag.Exception = TempData["errProduct"];
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

            if (TempData["EditProduct"] != null)
                ViewBag.Exception = TempData["EditProduct"].ToString();
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
           // product.Production_price = (from i in product.Products_Ingredients)

            if(originalProduct.Edit_time.ToString() != product.Edit_time.ToString())
            {
                //return Redirect(Request.UrlReferrer.ToString());
                TempData["EditProduct"] = "Data entry has been edited while on the edit page!\n" +
                    "Current State of the Entry: ";
                return RedirectToAction("Edit", new { id = originalProduct.Id });
            }
            product.Edit_time = DateTime.UtcNow;
            db.Entry(originalProduct).CurrentValues.SetValues(product);
            try
            {
                db.SaveChanges();
            }
            catch(Exception)
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

            var products_ingriedients = (from pi in db.Products_Ingredients
                                         where pi.ProductID == id
                                         select pi).ToList();
            
            try
            {
                foreach(var prod_ing in products_ingriedients)
                {
                    db.Database.ExecuteSqlCommand(@"UPDATE Ingredients
                    SET Stored_amount = Stored_amount + @p0 
                    WHERE Id = @p1", prod_ing.Required_ingredient_amount, prod_ing.IngredientID);
                }
                db.Products.Remove(product);
                db.SaveChanges();
            }
            catch(Exception e)
            {
                TempData["errProduct"] = e.InnerException.Message;
                return RedirectToAction("index");
            }
           
            return RedirectToAction("Index");
        }

        public ActionResult AddIngredient(int id)
        {
            if (TempData["errProduct_addIngredient"] != null)
                ViewBag.Exception = TempData["errProduct_addIngredient"];
            return View(new Models.Products_Ingredients()
            {
                ProductID = id
            });
        }

        [HttpPost]
        public ActionResult AddIngredient(Models.Products_Ingredients products_Ingredients)
        {
            if (!ModelState.IsValid)
                return View(products_Ingredients);
            //Get entries from database
            var ingredient = (from i in db.Ingredients
                              where i.Id == products_Ingredients.IngredientID
                              select i).First();

            var product = (from p in db.Products
                           where p.Id == products_Ingredients.ProductID
                           select p).First();
            try
            {
                //Check  for quantity of ingredients available
                if (ingredient.Stored_amount < products_Ingredients.Required_ingredient_amount)
                {
                    TempData["errProduct_addIngredient"] = "Number of requested ingredients is too big.";
                    return View(products_Ingredients);
                }

                //Perform necessary changes on entries without making changes on database
                ingredient.Stored_amount -= products_Ingredients.Required_ingredient_amount;
                product.Production_price += (int)(products_Ingredients.Required_ingredient_amount * ingredient.Price);

                //Perform changes on database and save them
                db.Products_Ingredients.Add(products_Ingredients);
                db.Entry(ingredient).CurrentValues.SetValues(ingredient);
                db.Entry(product).CurrentValues.SetValues(product);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                TempData["errProduct_addIngredient"] = e.Message;
                return View(products_Ingredients);
            }
            return RedirectToAction("Details", new { id = products_Ingredients.ProductID });
        }

        public ActionResult EditIngredientQuantity(int productID, int ingredientID)
        {
            var products_ingredients = (from p in db.Products_Ingredients
                                        where p.ProductID == productID && p.IngredientID == ingredientID
                                        select p).First();
            return View(products_ingredients);
        }

        [HttpPost]
        public ActionResult EditIngredientQuantity(Models.Products_Ingredients products_Ingredients)
        {
            if (!ModelState.IsValid)
                return View(products_Ingredients);

            var originalProducts_Ingredients = (from p in db.Products_Ingredients
                                                where p.ProductID == products_Ingredients.ProductID && p.IngredientID == products_Ingredients.IngredientID
                                                select p).First();

            db.Entry(originalProducts_Ingredients).CurrentValues.SetValues(products_Ingredients);
            try
            {
                db.SaveChanges();
            }
            catch(Exception e)
            {
                return View(products_Ingredients);
            }

            var product = (from p in db.Products
                           where p.Id == products_Ingredients.ProductID
                           select p).First();
            product.Production_price = (int)(from p in db.Products_Ingredients
                                             join i in db.Ingredients on p.IngredientID equals i.Id
                                             where p.ProductID == product.Id
                                             select p.Required_ingredient_amount * i.Price).Sum();

            var originalProduct = (from p in db.Products
                                   where p.Id == product.Id
                                   select p).First();

            db.Entry(originalProduct).CurrentValues.SetValues(product);
            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                ViewBag.Exception = e.Message;
                return View(products_Ingredients);
            }

            return RedirectToAction("Details", new { id = products_Ingredients.ProductID });
        }

        public ActionResult DeleteIngredientQuantity(int productID, int ingredientID)
        {
            var products_ingredients = (from p in db.Products_Ingredients
                                        where p.IngredientID == ingredientID && p.ProductID == productID
                                        select p).First();
            var ingredient = (from i in db.Ingredients
                              where i.Id == products_ingredients.IngredientID
                              select i).First();

            ingredient.Stored_amount += products_ingredients.Required_ingredient_amount;
            db.Entry(ingredient).CurrentValues.SetValues(ingredient);
            db.Products_Ingredients.Remove(products_ingredients);
            db.SaveChanges();
            return RedirectToAction("Details", new { id = products_ingredients.ProductID });
        }
    }
}
