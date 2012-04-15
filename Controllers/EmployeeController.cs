using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimesheetMVC.Models;
using PagedList;

namespace TimesheetMVC.Controllers
{ 
    public class EmployeeController : Controller
    {
        private TSDataEntities db = new TSDataEntities();

        //
        // GET: /Employee/
        [Authorize]
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date desc" : "Date";

            if (Request.HttpMethod == "GET")
            {
                searchString = currentFilter;
            }
            else
            {
                page = 1;
            }
            ViewBag.CurrentFilter = searchString;
            ViewBag.Image = "../../Content/images/rlombardo.jpg";

            var employees = from s in db.Employees
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                employees = employees.Where(s => s.LastName.ToUpper().Contains(searchString.ToUpper())
                                       || s.FirstName.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "Name desc":
                    employees = employees.OrderByDescending(s => s.LastName);
                    break;
                case "Start Date":
                    employees = employees.OrderBy(s => s.StartDate);
                    break; 
                default:
                    employees = employees.OrderBy(s => s.LastName);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(employees.ToPagedList(pageNumber, pageSize));
        }
           
        //
        // GET: /Employee/Details/5
        [Authorize]
        public ViewResult Details(int id)
        {
            Employee employee = db.Employees.Single(e => e.EmployeeID == id);
            return View(employee);
        }

        //
        // GET: /Employee/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Employee/Create
        [Authorize]
        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.AddObject(employee);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(employee);
        }
        
        //
        // GET: /Employee/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            Employee employee = db.Employees.Single(e => e.EmployeeID == id);
            return View(employee);
        }

        //
        // POST: /Employee/Edit/5
        [Authorize]
        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Attach(employee);
                db.ObjectStateManager.ChangeObjectState(employee, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        //
        // GET: /Employee/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            Employee employee = db.Employees.Single(e => e.EmployeeID == id);
            return View(employee);
        }

        //
        // POST: /Employee/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Employee employee = db.Employees.Single(e => e.EmployeeID == id);
            db.Employees.DeleteObject(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize]
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}