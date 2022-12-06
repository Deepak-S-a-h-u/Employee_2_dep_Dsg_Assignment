﻿using Emp_Dep_Dsg_Assignment.Data;
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
                                select new
                                {
                                    id = a.ID,
                                    name = a.Name,
                                    address = a.Address,
                                    departmentid = b.DepartmentID,
                                    department = c.DepName,
                                    designationid = d.ID,
                                    designation = d.DsgName
                                };
           /* employeeList.GroupBy(x => new { x.id, x.name, x.address, x.designation }).Select(x => new Employee
            {
                Name = x.Key.name,
                Employees=x.Select(Dept=>new EmpDep
                {
                    
                })*/
                
           /* });*/
           
            return Ok(employeeList);

        }
        [HttpPost]
        public IActionResult saveEmployees([FromBody] EmployeeDTO employeeDTO)
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

        [HttpPut]
        public IActionResult UpdateEmployee([FromBody] UpdateEmployeeDTO updateEmployeeDTO)
        {
            var empdata = _context.Employees.Find(updateEmployeeDTO.ID);
            if (empdata.ID == updateEmployeeDTO.ID)
            {
                empdata.Name = updateEmployeeDTO.Name;
                empdata.Address = updateEmployeeDTO.Address;
                empdata.DesignationID = updateEmployeeDTO.Designation;
            }
            _context.Employees.Update(empdata);
            _context.SaveChanges();

            var olddepartment = _context.EmpDep.Where(dep => dep.EmployeeID == updateEmployeeDTO.ID).Select(x=>x.DepartmentID).ToList();
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
                if(EmpDepinDB==null)
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
            var employeeInDb = _context.Employees.FirstOrDefault(employee => employee.ID == id);

            var depInEmployee = _context.EmpDep.Where(dep => dep.EmployeeID == id).Select(x => x.DepartmentID).ToList();
            foreach (var item in depInEmployee)
            {
                var empDep = _context.EmpDep.FirstOrDefault(dep => dep.EmployeeID == id && dep.DepartmentID == item);
            }

            return Ok(depInEmployee);

        }
    }
}



























/*foreach(var dep in employeeDTO.Department)
               {
               var Department = employeeDTO.Department[dep];
               }*/