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
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly PractialazureContext _context; // Replace 'YourDbContext' with your actual DbContext class

        public ProjectsController(PractialazureContext context)
        {
            _context = context;
        }

        // GET /api/projects
        [HttpGet]
        public IActionResult GetProjects()
        {
            var projects = _context.Projects.ToList();
            List<ProjectDTO> projectDTOs = new List<ProjectDTO>();

            foreach (var project in projects)
            {
                projectDTOs.Add(new ProjectDTO
                {
                    ProjectId = project.ProjectId,
                    ProjectName = project.ProjectName,
                    ProjectStartDate = project.ProjectStartDate,
                    ProjectEndDate = project.ProjectEndDate
                });
            }

            return Ok(projectDTOs);
        }

        // GET /api/projects/{id}
        [HttpGet("{id}")]
        public IActionResult GetProject(int id)
        {
            var project = _context.Projects.Find(id);

            if (project != null)
            {
                var projectDTO = new ProjectDTO
                {
                    ProjectId = project.ProjectId,
                    ProjectName = project.ProjectName,
                    ProjectStartDate = project.ProjectStartDate,
                    ProjectEndDate = project.ProjectEndDate
                };
                return Ok(projectDTO);
            }

            return NotFound();
        }

        // POST /api/projects
        [HttpPost]
        public IActionResult CreateProject(ProjectDTO projectDTO)
        {
            if (ModelState.IsValid)
            {
                var project = new Project
                {
                    ProjectName = projectDTO.ProjectName,
                    ProjectStartDate = projectDTO.ProjectStartDate,
                    ProjectEndDate = projectDTO.ProjectEndDate
                };

                _context.Projects.Add(project);
                _context.SaveChanges();

                return CreatedAtAction(nameof(GetProject), new { id = project.ProjectId }, new ProjectDTO
                {
                    ProjectId = project.ProjectId,
                    ProjectName = project.ProjectName,
                    ProjectStartDate = project.ProjectStartDate,
                    ProjectEndDate = project.ProjectEndDate
                });
            }

            return BadRequest(ModelState);
        }

        // PUT /api/projects/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateProject(int id, ProjectDTO projectDTO)
        {
            if (id != projectDTO.ProjectId)
            {
                return BadRequest();
            }

            var project = _context.Projects.Find(id);

            if (project == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                project.ProjectName = projectDTO.ProjectName;
                project.ProjectStartDate = projectDTO.ProjectStartDate;
                project.ProjectEndDate = projectDTO.ProjectEndDate;

                _context.SaveChanges();

                return NoContent();
            }

            return BadRequest(ModelState);
        }

        // DELETE /api/projects/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteProject(int id)
        {
            var project = _context.Projects.Find(id);

            if (project == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(project);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
