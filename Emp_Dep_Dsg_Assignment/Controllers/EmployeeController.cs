using Emp_Dep_Dsg_Assignment.Data;
using Emp_Dep_Dsg_Assignment.DTO;
using Emp_Dep_Dsg_Assignment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emp_Dep_Dsg_Assignment.Controllers
{
    [Route("api/Employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult getEmployees()
        {
            var employeeList = from a in _context.Employees
                               join b in _context.EmpDep
                               on a.ID equals b.EmployeeID
                               join c in _context.Departments
                               on b.DepartmentID equals c.ID
                               join d in _context.Designations
                                on a.DesignationID equals d.ID
                                select new GetEmployeeInfo()
                                {
                                    ID = a.ID,
                                    Name = a.Name,
                                    Address = a.Address,
                                    Departments = _context.EmpDep.Where(y => y.EmployeeID == a.ID).Select(y =>y.Department.DepName).ToList(),
                                    DesignationID = d.ID,
                                    Designation = d.DsgName
                                };
            List<GetEmployeeInfo> filteredList = new List<GetEmployeeInfo>();
            foreach (var item in employeeList)
            {
                var data = filteredList.FirstOrDefault(x => x.ID == item.ID);
                if(data == null)
                {
                    filteredList.Add(item);
                }
            };





            //var sorted = employeeList.GroupBy(x => new { x.id, x.name, x.address, x.designationid, x.designation })
            //  .Select(x => new GetEmployeeInfo
            //  {
            //      ID = x.Key.id,
            //      Name = x.Key.name,
            //      Address = x.Key.address,
            //      DesignationID = x.Key.designationid,
            //      Designation = x.Key.designation,
            //      Employees = _context.EmpDep.Where(y => y.EmployeeID == x.Key.id).Select(y=>y.Department.DepName).ToList()
            //  }) ;

            return Ok(filteredList);
}
        [HttpPost]
        public IActionResult saveEmployees([FromBody] EmployeeDTO employeeDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var employee = new Employee()
                {
                    Name = employeeDTO.Name,
                    Address = employeeDTO.Address,
                    DesignationID = employeeDTO.Designation,
                };
                _context.Add(employee);
                _context.SaveChanges();


                foreach (var item in employeeDTO.Department)
                {
                    var department = new EmpDep()
                    {
                        EmployeeID = employee.ID,
                        DepartmentID = item
                    };
                    _context.EmpDep.Add(department);
                    _context.SaveChanges();
                }
                return Ok();
            }   
        }

        [HttpPut]
        public IActionResult UpdateEmployee([FromBody] UpdateEmployeeDTO updateEmployeeDTO)
        {
            if (ModelState.IsValid)
            {
                var empdata = _context.Employees.Find(updateEmployeeDTO.ID);
                if (empdata.ID == updateEmployeeDTO.ID)
                {
                    empdata.Name = updateEmployeeDTO.Name;
                    empdata.Address = updateEmployeeDTO.Address;
                    empdata.DesignationID = updateEmployeeDTO.DesignationID;
                }
                _context.Employees.Update(empdata);
                _context.SaveChanges();

                var olddepartment = _context.EmpDep.Where(dep => dep.EmployeeID == updateEmployeeDTO.ID).Select(x => x.DepartmentID).ToList();
                List<EmpDep> empDeps = new List<EmpDep>();
                foreach (var item in olddepartment)
                {
                    var empDep = _context.EmpDep.FirstOrDefault(dep => dep.EmployeeID == updateEmployeeDTO.ID && dep.DepartmentID == item);
                    empDeps.Add(empDep);
                }
                _context.EmpDep.RemoveRange(empDeps);
                _context.SaveChanges();

                List<EmpDep> empDeps2 = new List<EmpDep>();
                foreach (var item in updateEmployeeDTO.Department)
                {
                    var EmpDepinDB = _context.EmpDep.FirstOrDefault(dep => dep.EmployeeID == updateEmployeeDTO.ID && dep.DepartmentID == item);
                    if (EmpDepinDB == null)
                    {
                        var department = new EmpDep()
                        {
                            EmployeeID = updateEmployeeDTO.ID,
                            DepartmentID = item
                        };
                        empDeps2.Add(department);
                    }
                }
                _context.EmpDep.AddRange(empDeps2);
                _context.SaveChanges();
                return Ok();
            }

            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var employeeInDb = _context.Employees.FirstOrDefault(employee => employee.ID == id);
            
            var depInEmployee = _context.EmpDep.Where(dep => dep.EmployeeID ==id).Select(x => x.DepartmentID).ToList();
            foreach (var item in depInEmployee)
            {
                var empDep = _context.EmpDep.FirstOrDefault(dep => dep.EmployeeID == id && dep.DepartmentID == item);
                _context.EmpDep.Remove(empDep);
                _context.SaveChanges();
            }
           
            return Ok();
            
        }
        [HttpGet("{id}")]
        public IActionResult GetEmployee(int id)
        {
            var employeeList = from a in _context.Employees
                              
                               join b in _context.EmpDep
                               on a.ID equals b.EmployeeID
                               join c in _context.Departments
                               on b.DepartmentID equals c.ID
                               join d in _context.Designations
                                on a.DesignationID equals d.ID
                               where(a.ID==id)
                               select new 
                               {
                                   ID = a.ID,
                                   Name = a.Name,
                                   Address = a.Address,
                                   Department = _context.EmpDep.Where(y => y.EmployeeID == a.ID).Select(y => y.Department.ID).ToList(),
                                   DesignationID = d.ID,
                                 
                               };
            var x = employeeList.FirstOrDefault(x => x.ID == id);
            return Ok(x);




            //var sorted = employeeList.GroupBy(x => new { x.id, x.name, x.address, x.designationid, x.designation })
            //  .Select(x => new GetEmployeeInfo
            //  {
            //      ID = x.Key.id,
            //      Name = x.Key.name,
            //      Address = x.Key.address,
            //      DesignationID = x.Key.designationid,
            //      Designation = x.Key.designation,
            //      Employees = _context.EmpDep.Where(y => y.EmployeeID == x.Key.id).Select(y=>y.Department.DepName).ToList()
            //  }) ;

            /*return Ok(employeeList);*/
        }




        /*{
            var employeeInDb = _context.Employees.FirstOrDefault(employee => employee.ID == id);

            var depInEmployee = _context.EmpDep.Where(dep => dep.EmployeeID == id).Select(x => x.DepartmentID).ToList();
            foreach (var item in depInEmployee)
            {
                var empDep = _context.EmpDep.FirstOrDefault(dep => dep.EmployeeID == id && dep.DepartmentID == item);
            }

           

            return Ok(depInEmployee);
            
        }*/
    }
}
