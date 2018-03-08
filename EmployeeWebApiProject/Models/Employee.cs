using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeWebApiProject.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public bool Active { get; set; }
    }
}