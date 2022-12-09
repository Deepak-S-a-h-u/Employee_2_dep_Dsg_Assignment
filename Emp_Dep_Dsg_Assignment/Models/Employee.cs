using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Emp_Dep_Dsg_Assignment.Models
{
    public class Employee
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Please Enter Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Enter Address")]
        public string Address { get; set; }
        
        [Required(ErrorMessage = "Designation required")]

        public int DesignationID { get; set; }
        [ForeignKey("DesignationID")]
        public Designation Designation { get; set; }


        public ICollection<EmpDep> Employees { get; set; }
    }
}
