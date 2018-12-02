using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Database_semester_project.Models
{
    public partial class Orders
    {
        private readonly FactoryEntities db = new FactoryEntities();

        public List<SelectListItem> CustomersNames
        {
            get
            {
                var customers = (from c in db.Customers
                                 select c).ToList();
                List<SelectListItem> list = new List<SelectListItem>();
                foreach (var c in customers) 
                {
                    list.Add(new SelectListItem()
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    });
                }
                return list;
            }
        }

        public List<SelectListItem> ProductsNames
        {
            get
            {
                var products = (from p in db.Products
                                select p).ToList();
                List<SelectListItem> list = new List<SelectListItem>();
                foreach (var p in products)
                {
                    list.Add(new SelectListItem()
                    {
                        Value = p.Id.ToString(),
                        Text = p.Name
                    });
                }
                return list;
            }
        }
    }
}