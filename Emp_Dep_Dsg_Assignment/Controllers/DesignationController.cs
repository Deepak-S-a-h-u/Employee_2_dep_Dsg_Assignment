using Emp_Dep_Dsg_Assignment.Data;
using Emp_Dep_Dsg_Assignment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emp_Dep_Dsg_Assignment.Controllers
{
    [Route("api/Designation")]
    [ApiController]
    public class DesignationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public DesignationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetDesignations()
        {
            return Ok(_context.Designations.ToList());
        }

        [HttpPost]
        public IActionResult AddDesignation([FromBody]Designation designation)
        {
           if(designation !=null && ModelState.IsValid)
            {
                _context.Designations.Add(designation);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();
         }
       [HttpPut]
       public IActionResult UpdateDesignation([FromBody]Designation designation)
        {
            if(ModelState.IsValid)
            {
                _context.Designations.Update(designation);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public IActionResult GetDesignation(int id)
        {
            var designationInDb = _context.Designations.Find(id);
            if (designationInDb == null)
                return NotFound();
            return Ok(designationInDb);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteDesignation(int id)
        {
            var designationindb = _context.Designations.Find(id);
            if (designationindb != null)
            {
                _context.Designations.Remove(designationindb);
                _context.SaveChanges();
                return Ok();
            }
            return NotFound();
        }
    }
}
