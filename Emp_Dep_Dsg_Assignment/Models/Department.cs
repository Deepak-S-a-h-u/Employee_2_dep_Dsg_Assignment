using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Emp_Dep_Dsg_Assignment.Models
{
    public class Department
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Please Enter Department")]
        [Display(Name ="Department")]
        public string DepName { get; set; }

        public ICollection<EmpDep> EmpDep { get; set; }
    }
}
