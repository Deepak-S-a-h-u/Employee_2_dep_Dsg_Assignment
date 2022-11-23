using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Emp_Dep_Dsg_Assignment.DTO
{
    public class DepartmentDTO
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Please Enter Department")]
        [Display(Name = "Department")]
        public string DepName { get; set; }
    }
}
