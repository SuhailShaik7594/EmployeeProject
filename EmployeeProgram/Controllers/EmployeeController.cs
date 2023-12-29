// EmployeeController.cs

using EmployeeProgram.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EmployeeProgram.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeContext _employeeContext;

        public EmployeeController(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
        }

        [HttpGet]
        [Route("GetEmployees")]
        public List<Employees> GetEmployees()
        {
            return _employeeContext.Employees.ToList();
        }

        [HttpGet]
        [Route("GetEmployee")]
        public IActionResult GetEmployee(int id)
        {
            var employee = _employeeContext.Employees.FirstOrDefault(x => x.Id == id);

            if (employee != null)
            {
                return Ok(employee);
            }
            else
            {
                return NotFound("Employee not found");
            }
        }

        [HttpPost]
        [Route("AddEmployees")]
        public string AddEmployee(Employees employee)
        {
            _employeeContext.Employees.Add(employee);
            _employeeContext.SaveChanges();
            return "Employee Added";
        }

        [HttpPut]
        [Route("UpdateEmployees")]
        public string UpdateEmployee(Employees employee)
        {
            _employeeContext.Entry(employee).State = EntityState.Modified;
            _employeeContext.SaveChanges();
            return "Employee Updated";
        }

        [HttpDelete]
        [Route("DeleteEmployee")]
        public string DeleteEmployee(int id)
        {
            var employee = _employeeContext.Employees.Where(x => x.Id == id).FirstOrDefault();

            if (employee != null)
            {
                _employeeContext.Employees.Remove(employee);
                _employeeContext.SaveChanges();
                return "Employee Deleted";
            }
            else
            {
                return "No Employee Found";
            }
        }
    }
}

