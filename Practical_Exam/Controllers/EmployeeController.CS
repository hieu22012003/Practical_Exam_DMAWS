using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practical_Exam.Dtos;
using Practical_Exam.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Practical_Exam.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly PractialazureContext _context; // Replace 'YourDbContext' with your actual DbContext class

        public EmployeeController(PractialazureContext context)
        {
            _context = context;
        }

        // GET /api/employee
        [HttpGet]
        public IActionResult GetEmployees()
        {
            var employees = _context.Employees.ToList();
            List<EmployeeDTO> employeeDTOs = new List<EmployeeDTO>();

            foreach (var employee in employees)
            {
                employeeDTOs.Add(new EmployeeDTO
                {
                    EmployeeId = employee.EmployeeId,
                    EmployeeName = employee.EmployeeName,
                    EmployeeDOB = employee.EmployeeDob,
                    EmployeeDepartment = employee.EmployeeDepartment
                });
            }

            return Ok(employeeDTOs);
        }

        // GET /api/employee/{id}
        [HttpGet("{id}")]
        public IActionResult GetEmployee(int id)
        {
            var employee = _context.Employees.Find(id);

            if (employee != null)
            {
                var employeeDTO = new EmployeeDTO
                {
                    EmployeeId = employee.EmployeeId,
                    EmployeeName = employee.EmployeeName,
                    EmployeeDOB = employee.EmployeeDob,
                    EmployeeDepartment = employee.EmployeeDepartment
                };
                return Ok(employeeDTO);
            }

            return NotFound();
        }

        // POST /api/employee
        [HttpPost]
        public IActionResult CreateEmployee(EmployeeDTO employeeDTO)
        {
            if (ModelState.IsValid)
            {
                var maxEmployeeId = _context.Employees.Max(e => e.EmployeeId);

                var employee = new Employee
                {
                    EmployeeId = maxEmployeeId + 1,
                    EmployeeName = employeeDTO.EmployeeName,
                    EmployeeDob = employeeDTO.EmployeeDOB,
                    EmployeeDepartment = employeeDTO.EmployeeDepartment
                };

                _context.Employees.Add(employee);
                _context.SaveChanges();

                return CreatedAtAction(nameof(GetEmployee), new { id = employee.EmployeeId }, new EmployeeDTO
                {
                    EmployeeId = employee.EmployeeId,
                    EmployeeName = employee.EmployeeName,
                    EmployeeDOB = employee.EmployeeDob,
                    EmployeeDepartment = employee.EmployeeDepartment
                });
            }

            return BadRequest(ModelState);
        }

        // PUT /api/employee/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id, EmployeeDTO employeeDTO)
        {
            if (id != employeeDTO.EmployeeId)
            {
                return BadRequest();
            }

            var employee = _context.Employees.Find(id);

            if (employee == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                employee.EmployeeName = employeeDTO.EmployeeName;
                employee.EmployeeDob = employeeDTO.EmployeeDOB;
                employee.EmployeeDepartment = employeeDTO.EmployeeDepartment;

                _context.SaveChanges();

                return NoContent();
            }

            return BadRequest(ModelState);
        }

        // DELETE /api/employee/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var employee = _context.Employees.Find(id);

            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
