using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Database_semester_project.Models
{
    public partial class Products_Ingredients
    {
        private readonly Models.FactoryEntities db = new FactoryEntities();
        public string IngredientName
        {
            get
            {
                var name = (from i in db.Ingredients
                            where IngredientID == i.Id
                            select i.Name).First();
                return name;
            }
        }

        public string ProductName
        {
            get
            {
                var name = (from p in db.Products
                            where ProductID == p.Id
                            select p.Name).First();
                return name;
            }
        }

        public List<SelectListItem> ProductsNames
        {
            get
            {
                var names = (from p in db.Products
                             select p).ToList();
                List<SelectListItem> list = new List<SelectListItem>();
                foreach(var n in names)
                {
                    list.Add(new SelectListItem()
                    {
                        Text = n.Name,
                        Value = n.Id.ToString()
                    });
                }
                return list;
            }
        }

        public List<SelectListItem> IngredientsNames
        {
            get
            {
                var names = (from i in db.Ingredients
                             select i).ToList();
                List<SelectListItem> list = new List<SelectListItem>();
                foreach(var n in names)
                {
                    list.Add(new SelectListItem()
                    {
                        Text = n.Name,
                        Value = n.Id.ToString()
                    });
                }
                return list;
            }
        }
    }
}