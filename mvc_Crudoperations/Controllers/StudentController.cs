using mvc_Crudoperations.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvc_Crudoperations.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetData()
        {
            using (DBModel db = new DBModel())
            {
                List<StudentTable> stdlist = db.StudentTables.ToList<StudentTable>();
                return Json(new { data = stdlist }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new StudentTable());
            else
            {
                using (DBModel db = new DBModel())
                {
                    return View(db.StudentTables.Where(x => x.StudentId == id).FirstOrDefault<StudentTable>());
                }
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(StudentTable emp)
        {
            using (DBModel db = new DBModel())
            {
                if (emp.StudentId == 0)
                {
                    db.StudentTables.Add(emp);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    db.Entry(emp).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
                }
            }


        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (DBModel db = new DBModel())
            {
                StudentTable emp = db.StudentTables.Where(x => x.StudentId == id).FirstOrDefault<StudentTable>();
                db.StudentTables.Remove(emp);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}