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
    [Route("api/projectemployee")]
    [ApiController]
    public class ProjectEmployeeController : ControllerBase
    {
        private readonly PractialazureContext _context; // Replace 'YourDbContext' with your actual DbContext class

        public ProjectEmployeeController(PractialazureContext context)
        {
            _context = context;
        }

        // GET /api/projectemployee
        [HttpGet]
        public IActionResult GetProjectEmployees()
        {
            var projectEmployees = _context.ProjectEmployees.ToList();
            List<ProjectEmployeeDTO> projectEmployeeDTOs = new List<ProjectEmployeeDTO>();

            foreach (var projectEmployee in projectEmployees)
            {
                projectEmployeeDTOs.Add(new ProjectEmployeeDTO
                {
                    EmployeeId = projectEmployee.EmployeeId,
                    ProjectId = projectEmployee.ProjectId,
                    Tasks = projectEmployee.Tasks
                });
            }

            return Ok(projectEmployeeDTOs);
        }

        // GET /api/projectemployee/{id}
        [HttpGet("{id}")]
        public IActionResult GetProjectEmployee(int id)
        {
            var projectEmployee = _context.ProjectEmployees.Find(id);

            if (projectEmployee != null)
            {
                var projectEmployeeDTO = new ProjectEmployeeDTO
                {
                    EmployeeId = projectEmployee.EmployeeId,
                    ProjectId = projectEmployee.ProjectId,
                    Tasks = projectEmployee.Tasks
                };
                return Ok(projectEmployeeDTO);
            }

            return NotFound();
        }

        // POST /api/projectemployee
        [HttpPost]
        public IActionResult CreateProjectEmployee(ProjectEmployeeDTO projectEmployeeDTO)
        {
            if (ModelState.IsValid)
            {
                var projectEmployee = new ProjectEmployee
                {
                    EmployeeId = projectEmployeeDTO.EmployeeId,
                    ProjectId = projectEmployeeDTO.ProjectId,
                    Tasks = projectEmployeeDTO.Tasks
                };

                _context.ProjectEmployees.Add(projectEmployee);
                _context.SaveChanges();

                return CreatedAtAction(nameof(GetProjectEmployee), new { id = projectEmployee.EmployeeId }, new ProjectEmployeeDTO
                {
                    EmployeeId = projectEmployee.EmployeeId,
                    ProjectId = projectEmployee.ProjectId,
                    Tasks = projectEmployee.Tasks
                });
            }

            return BadRequest(ModelState);
        }

        // PUT /api/projectemployee/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateProjectEmployee(int id, ProjectEmployeeDTO projectEmployeeDTO)
        {
            if (id != projectEmployeeDTO.EmployeeId)
            {
                return BadRequest();
            }

            var projectEmployee = _context.ProjectEmployees.Find(id);

            if (projectEmployee == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                projectEmployee.EmployeeId = projectEmployeeDTO.EmployeeId;
                projectEmployee.ProjectId = projectEmployeeDTO.ProjectId;
                projectEmployee.Tasks = projectEmployeeDTO.Tasks;

                _context.SaveChanges();

                return NoContent();
            }

            return BadRequest(ModelState);
        }

        // DELETE /api/projectemployee/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteProjectEmployee(int id)
        {
            var projectEmployee = _context.ProjectEmployees.Find(id);

            if (projectEmployee == null)
            {
                return NotFound();
            }

            _context.ProjectEmployees.Remove(projectEmployee);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
