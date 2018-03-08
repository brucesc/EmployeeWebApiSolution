using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeWebApiProject.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int EmployeeId { get; set; }
        public decimal Budget { get; set; }

        public virtual Employee employee { get; set; }
    }
}