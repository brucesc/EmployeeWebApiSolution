using EmployeeWebApiProject.Models;
using EmployeeWebApiProject.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace EmployeeWebApiProject.Controllers
{
    public class DepartmentsController : Controller
    {
        public AppDbContext db = new AppDbContext();

        public ActionResult Search(string searchCriteria)
        {
            if (searchCriteria == null)
            {
                return Json(new JsonMessage("Failure", "SearchCriteria is null"), JsonRequestBehavior.AllowGet);
            }
            List<Department> departments = db.Departments.Where(d => d.Name.Contains(searchCriteria)).ToList();
            return Json(departments, JsonRequestBehavior.AllowGet);
        }

        public ActionResult List()
        {
            return Json(db.Departments.ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Get(int? id)
        {
            if (id == null)
            {
                return Json(new JsonMessage("Failure", "Id is null"), JsonRequestBehavior.AllowGet);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return Json(new JsonMessage("Failure", "Id is not found"), JsonRequestBehavior.AllowGet);
            }
            return Json(department, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create([FromBody] Department department)
        {
            if (!ModelState.IsValid)
            {
                return Json(new JsonMessage("Failure", "MobelState is not valid"), JsonRequestBehavior.AllowGet);
            }
            db.Departments.Add(department);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage("Success", "Department was created."));
        }

        public ActionResult Change([FromBody] Department department)
        {
            Department tempDepartment = db.Departments.Find(department.Id);
            tempDepartment.Name = department.Name;
            tempDepartment.EmployeeId = department.EmployeeId;
            tempDepartment.Budget = department.Budget;
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage("Success", "Department was changed."));
        }

        public ActionResult Remove([FromBody] Department department)
        {
            Department tempDepartment = db.Departments.Find(department.Id);
            db.Departments.Remove(tempDepartment);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage("Success", "Department has been removed"), JsonRequestBehavior.AllowGet);
        }
    }

}