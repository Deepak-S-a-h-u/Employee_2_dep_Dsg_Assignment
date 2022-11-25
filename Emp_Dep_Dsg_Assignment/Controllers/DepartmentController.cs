using Emp_Dep_Dsg_Assignment.Data;
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
    [Route("api/Department")]
    [ApiController]
    public class DepartmentController : Controller
    {
        private readonly ApplicationDbContext _context;
        public DepartmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetDepartment()
        {
            return Json(_context.Departments.ToList());
        }

        [HttpPost]
        public IActionResult AddDepartment([FromBody] Department department)
        {
            if (department != null && ModelState.IsValid)
            {
                _context.Departments.Add(department);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut]
        public IActionResult UpdateDepartment(Department department)
        {
            if (ModelState.IsValid)
            {
                _context.Departments.Update(department);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteDepartment(int id)
        {
            var departmentindb = _context.Departments.Find(id);
            if (departmentindb != null)
            {
                _context.Departments.Remove(departmentindb);
                _context.SaveChanges();
                return Ok();
            }
            return NotFound();
        }
    }
}
