using Emp_Dep_Dsg_Assignment.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Emp_Dep_Dsg_Assignment.DTO
{
    public class GetEmployeeInfo
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Please Enter Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Enter Address")]
        public string Address { get; set; }
        public int[] Department { get; set; }
        public int DesignationID { get; set; }
        public string Designation { get; set; }


        public List<string> Departments { get; set; }

    }
}
