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
    public class EmployeesController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // /Employees/ActiveEmployees
        public ActionResult ActiveEmployees()
        {
            List<Employee> employees = db.Employees.Where(e => e.Active).ToList();
            return Json(employees, JsonRequestBehavior.AllowGet);
        }

        // /Employees/List
        public ActionResult List()
        {
            return Json(db.Employees.ToList(), JsonRequestBehavior.AllowGet); // this goes in every controller for the PRS
        }

        // /Employees/Get/5
        public ActionResult Get(int? id)
        {
            if (id == null)
            {
                return Json(new JsonMessage("Failure", "Id is null"), JsonRequestBehavior.AllowGet);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return Json(new JsonMessage("Failure", "Id is not found"), JsonRequestBehavior.AllowGet);
            }
            return Json(employee, JsonRequestBehavior.AllowGet);

        }

        // /Employees/Create [POST]
        public ActionResult Create([FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return Json(new JsonMessage("Failure", "ModelState is not valid"), JsonRequestBehavior.AllowGet);
            }
            db.Employees.Add(employee);
            try
            {
                db.SaveChanges();
            } catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage("Success", "Employee was created.")); // add to this return the employee generated Id
        }

        // /Employees/Change [POST]
        public ActionResult Change([FromBody] Employee employee)
        {
            Employee tempEmployee = db.Employees.Find(employee.Id);
            if (tempEmployee == null)
            {
                return Json(new JsonMessage("Failure", "Record to be changed has been deleted"), JsonRequestBehavior.AllowGet);
            }
            tempEmployee.Name = employee.Name;
            tempEmployee.Salary = employee.Salary;
            tempEmployee.Active = employee.Active;
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage("Success", "Employee was changed."));
        }

        // /Employees/Remove/5
        public ActionResult Remove([FromBody] Employee employee)
        {
            Employee tempEmployee = db.Employees.Find(employee.Id);
            db.Employees.Remove(tempEmployee);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage("Success", "Employee was removed.")); 
        }

    }
}