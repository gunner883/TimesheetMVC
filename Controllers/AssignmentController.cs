using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimesheetMVC.Models;

namespace TimesheetMVC.Controllers
{ 
    public class AssignmentController : Controller
    {
        private tsdataEntities db = new tsdataEntities();

        //
        // GET: /Assignment/
        [Authorize]
        public ViewResult Index()
        {
            return View(db.Assignments.ToList());
        }
        //
        // GET: /Assignment/Details/5

        public ViewResult Details(int id)
        {
            Assignment Assignment = db.Assignments.Single(t => t.AssignmentID == id);
            return View(Assignment);
        }

        //
        // GET: /Assignment/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Assignment/Create

        [HttpPost]
        public ActionResult Create(Assignment Assignment)
        {
            if (ModelState.IsValid)
            {
                db.Assignments.AddObject(Assignment);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(Assignment);
        }
        
        //
        // GET: /Assignment/Edit/5
 
        public ActionResult Edit(int id)
        {
            Assignment Assignment = db.Assignments.Single(t => t.AssignmentID == id);
            return View(Assignment);
        }

        //
        // POST: /Assignment/Edit/5

        [HttpPost]
        public ActionResult Edit(Assignment Assignment)
        {
            if (ModelState.IsValid)
            {
                db.Assignments.Attach(Assignment);
                db.ObjectStateManager.ChangeObjectState(Assignment, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Assignment);
        }

        //
        // GET: /Assignment/Delete/5
 
        public ActionResult Delete(int id)
        {
            Assignment Assignment = db.Assignments.Single(t => t.AssignmentID == id);
            return View(Assignment);
        }

        //
        // POST: /Assignment/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Assignment Assignment = db.Assignments.Single(t => t.AssignmentID == id);
            db.Assignments.DeleteObject(Assignment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}