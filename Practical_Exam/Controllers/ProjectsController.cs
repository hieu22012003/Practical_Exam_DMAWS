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
        [HttpGet]
        public IActionResult Index()
        {
            var projects = _context.Projects.ToList<Project>();
            return Ok(projects);
        }
        [HttpGet]
        [Route("detail")]
        public IActionResult Details(int id)
        {
            var pr = _context.Projects
        .Include(p => p.ProjectEmployees)
        .ThenInclude(pe => pe.Employee)
        .FirstOrDefaultAsync(p => p.ProjectId == id);

            if (pr == null)
            {
                return NotFound();
            }
            return Ok(pr);
        }

        [HttpGet]
        [Route("search")]
        public IActionResult SearchByName(string projectName, bool inProgress)
        {
            IQueryable<Project> query = _context.Projects;

            if (!string.IsNullOrEmpty(projectName))
            {
                var pr = query.Where(p => p.ProjectName.Contains(projectName)).FirstOrDefault();
                return Ok(pr);
            }

            if (inProgress)
            {
                var pr = query.Where(p => p.ProjectEndDate == null || p.ProjectEndDate > DateTime.Now).ToList();
                return Ok(pr);
            }
            else
            {
                var pr = query.Where(p => p.ProjectEndDate != null && p.ProjectEndDate <= DateTime.Now).ToList();
                return Ok(pr);

            }



        }

        [HttpGet]
        [Route("SearchProgress")]
        public IActionResult SearchProgress(string projectName)
        {
            var pr = _context.Projects.Where(p => p.ProjectName.Contains(projectName)).FirstOrDefault();
            return Ok(new ProjectDTO
            {
                ProjectName = pr.ProjectName,
                ProjectStartDate = pr.ProjectStartDate,
                ProjectEndDate = pr.ProjectEndDate,
            });
        }


        [HttpPost]
        public IActionResult Create(ProjectDTO projectData)
        {
            var pr = _context.Projects.Where(p => p.ProjectName.Contains(projectData.ProjectName)).FirstOrDefault();
            if (pr != null)
                return BadRequest("Project is exists");
            var newpr = new Entities.Project
            {
                ProjectName = projectData.ProjectName,
                ProjectStartDate = projectData.ProjectStartDate,
                ProjectEndDate = projectData.ProjectEndDate,
            };
            _context.Projects.Add(newpr);
            _context.SaveChanges();
            return Ok(projectData);
        }

        [HttpPut]
        public IActionResult Update(int id, ProjectDTO projectData)
        {
            var pr = _context.Projects.Find(id);
            if (pr == null) return NotFound("Not Found Project");
            pr.ProjectStartDate = projectData.ProjectStartDate;
            pr.ProjectEndDate = projectData.ProjectEndDate;
            pr.ProjectName = pr.ProjectName;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delette(int id)
        {
            var pr = _context.Projects.Find(id);
            if (pr == null) return NotFound("Not Found Project");
            _context.Projects.Remove(pr);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
