using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Emp_Dep_Dsg_Assignment.Models
{
    public class EmpDep
    {

       
        public int DepartmentID { get; set; }
        public Department Department { get; set; }

        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }

        


    }
}
