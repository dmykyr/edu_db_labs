using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DbREstService.Data;
using DbREstService.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace DbREstService.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly ProjectDbContext _context;

        public ProjectsController(ProjectDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            try
            {
                return await _context.Projects.ToListAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("{projectId}")]
        public async Task<ActionResult<Project>> GetProject(int projectId)
        {
            try
            {
                var project = await _context.Projects.FindAsync(projectId);

                if (project == null)  return NotFound("Project with such Id does not exist");

                return project;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPatch("{projectId}")]
        public IActionResult PatchProject(int projectId, [FromBody] JsonPatchDocument<Project> patchDoc)
        {
            var project = _context.Projects.Find(projectId);

            if (project == null)
            {
                return NotFound("Project with such Id does not exist");
            }

            patchDoc.ApplyTo(project, ModelState);
            _context.SaveChanges();

            return RedirectToAction("GetProject", new { projectId });
        }

        [HttpPost]
        public async Task<ActionResult<Project>> CreateProject([FromBody] Project project)
        {
            try
            {
                if (await _context.Projects.FirstAsync(p => p.Name == project.Name) != null)
                {
                    return BadRequest("DuplicateProjectNameError: project with such name already exists");
                }

                _context.Projects.Add(project);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetProject", new { id = project.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpDelete("{projectId}")]
        public async Task<IActionResult> DeleteProject(int projectId)
        {
            try
            {
                var project = await _context.Projects.FindAsync(projectId);
                if (project == null)
                {
                    return NotFound();
                }

                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetProject", new { id = project.Id }, project);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
