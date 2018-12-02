using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Database_semester_project.Models
{
    public partial class Tasks
    {
        private readonly FactoryEntities db = new FactoryEntities();
        public bool ProductLocked { get; set; }
        public string ProductName
        {
            get
            {
                var name = (from p in db.Products
                            where p.Id == ProductID
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
                foreach (var n in names)
                {
                    list.Add(new SelectListItem()
                    {
                        Value = n.Id.ToString(),
                        Text = n.Name
                    });
                }
                return list;

            }
        }
        public List<SelectListItem> AvaiableEmployees
        {
            get
            {
                var employees = (from e in db.Employees
                                 where e.Tasks.Count == 0
                                 select e).ToList();
                List<SelectListItem> list = new List<SelectListItem>();
                foreach (var e in employees)
                {
                    list.Add(new SelectListItem()
                    {
                        Value = e.Id.ToString(),
                        Text = e.First_name + " " + e.Last_name
                    });
                }
                return list;
            }
        }
    }
}