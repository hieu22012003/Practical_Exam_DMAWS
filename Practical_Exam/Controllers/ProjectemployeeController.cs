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
    [ApiController]
    [Route("projectemployee")]
    public class ProjectemployeeController : ControllerBase
    {
        private readonly PractialazureContext _context;
        public ProjectemployeeController(PractialazureContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index(string projectName)
        {
            var pr = _context.ProjectEmployees
                .Include(p => p.Employee)
                .Include(p => p.Project)
                .FirstOrDefault();
            return Ok(pr);
        }
    }
}
