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

        [HttpGet("{projectId:int}")]
        public async Task<ActionResult<Project>> GetProject(
            [FromQuery(Name = "projectId")] int projectId)
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

        [HttpPatch("{projectId:int}")]
        public async Task<ActionResult<Project>> PatchProject(
            [FromQuery(Name = "pageNumber")] int projectId, 
            [FromBody] JsonPatchDocument<Project> patchDoc)
        {
            var project = await _context.Projects.FindAsync(projectId);

            if (project == null)
            {
                return NotFound("Project with such Id does not exist");
            }

            patchDoc.ApplyTo(project, ModelState);
            await _context.SaveChangesAsync();

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

        [HttpDelete("{projectId:int}")]
        public async Task<IActionResult> DeleteProject(
            [FromQuery(Name = "projectId")] int projectId)
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

        [HttpGet("{projectId:int}/reviews")]
        public async Task<ActionResult<IEnumerable<Review>>> GetProjectReviews(
            [FromQuery(Name = "projectId")] int projectId)
        {
            try
            {
                var project = await _context.Projects.FindAsync(projectId);

                if (project == null) return NotFound("Project with such Id does not exist");

                if (project.Status != "Finished") return BadRequest("Selected project is not finished");

                var reviews = await _context.Reviews
                                            .Where(r => r.ProjectId == projectId)
                                            .ToListAsync();

                await _context.SaveChangesAsync();

                return CreatedAtAction("GetReviews", new { id = project.Id }, project);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPost("{projectId:int}/reviews")]
        public async Task<ActionResult<IEnumerable<Review>>> AddReview(
            [FromQuery(Name = "projectId")] int projectId, 
            [FromBody] Review review)
        {
            try
            {
                var project = await _context.Projects.FindAsync(projectId);

                if (project == null) return NotFound("Project with such Id does not exist");

                if (project.Status != "Finished") return BadRequest("Selected project is not finished");

                _context.Reviews.Add(review);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetReviews", new { id = project.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
