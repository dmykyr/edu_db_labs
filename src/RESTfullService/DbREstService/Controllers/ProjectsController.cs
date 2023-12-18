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

        /// <summary>
        ///     Retrieves all projects
        /// </summary>
        /// <response code="200"> Project list </response>
        /// <response code="500"> Internal Server Error </response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            try
            {
                return await _context.Projects
                    .Include(p => p.Tasks)
                    .Include(p => p.Payments)
                    .Include(p => p.Reviews)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        /// <summary>
        ///     Retrieves a specific project by unique id
        /// </summary>
        /// <response code="200"> Specific Project </response>
        /// <response code="400"> InvalidIndexError: Project with such Id does not exist </response>
        /// <response code="500"> Internal Server Error </response>
        [HttpGet("{projectId:int}")]
        public async Task<ActionResult<Project>> GetProject(int projectId)
        {
            try
            {
                var project = await _context.Projects
                    .Include(p => p.Tasks)
                    .Include(p => p.Payments)
                    .Include(p => p.Reviews)
                    .FirstAsync(p => p.Id == projectId);


                if (project == null)
                {
                    return BadRequest("InvalidIndexError: Project with such Id does not exist");
                }

                return project;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        /// <summary>
        ///     Update specific values for project by unique id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     [
        ///       {
        ///         "op":"replace",
        ///         "path":"/{parameter name}",
        ///         "value": "{parameter value}"
        ///       }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200"> An updated Project </response>
        /// <response code="400"> InvalidIndexError: Project with such Id does not exist </response>
        /// <response code="500"> Internal Server Error </response>
        [HttpPatch("{projectId:int}")]
        public async Task<ActionResult<Project>> PatchProject(
            int projectId, 
            [FromBody] JsonPatchDocument<Project> patchDoc)
        {
            try
            {
                var project = await _context.Projects
                    .Include(p => p.Tasks)
                    .Include(p => p.Payments)
                    .Include(p => p.Reviews)
                    .FirstAsync(p => p.Id == projectId);

                if (project == null)
                {
                    return BadRequest("InvalidIndexError: Project with such Id does not exist");
                }

                patchDoc.ApplyTo(project, ModelState);
                await _context.SaveChangesAsync();

                return RedirectToAction("GetProject", new { projectId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Add Project to db
        /// </summary>
        /// <response code="200"> A newly created Project </response>
        /// <response code="400">
        ///     DuplicateProjectNameError: project with such name already exists&#xA;
        ///     InvalidIndexError: Project with such Id does not exist
        /// </response>
        /// <response code="500"> Internal Server Error </response>
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

        /// <summary>
        ///     Remove Project from db by unique id
        /// </summary>
        /// <response code="200"> Just removed Project </response>
        /// <response code="400"> InvalidIndexError: Project with such Id does not exist </response>
        /// <response code="500"> Internal Server Error </response>
        [HttpDelete("{projectId:int}")]
        public async Task<ActionResult<Project>> DeleteProject(int projectId)
        {
            try
            {
                var project = await _context.Projects
                    .Include(p => p.Tasks)
                    .Include(p => p.Payments)
                    .Include(p => p.Reviews)
                    .FirstAsync(p => p.Id == projectId);

                if (project == null)
                {
                    return BadRequest("InvalidIndexError: Project with such Id does not exist");
                }

                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();

                return project;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        /// <summary>
        ///     Retrieves reviews related to specific project
        /// </summary>
        /// <response code="200"> List of reviews </response>
        /// <response code="400">
        ///     InvalidIndexError: Project with such Id does not exist&#xA;
        ///     Selected project is not finished
        /// </response>
        /// <response code="500"> Internal Server Error </response>
        [HttpGet("{projectId:int}/reviews")]
        public async Task<ActionResult<IEnumerable<Review>>> GetProjectReviews(int projectId)
        {
            try
            {
                var project = await _context.Projects.FindAsync(projectId);
                if (project == null)
                {
                    return BadRequest("InvalidIndexError: Project with such Id does not exist");
                }

                if (project.Status != "Finished")
                {
                    return BadRequest("Selected project is not finished");
                }

                var reviews = await _context.Reviews
                                            .Where(r => r.ProjectId == projectId)
                                            .ToListAsync();
                return reviews;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        /// <summary>
        ///     Add review to specific project
        /// </summary>
        /// <response code="200"> List of reviews </response>
        /// <response code="400">
        ///     InvalidIndexError: Project with such Id does not exist&#xA;
        ///     Selected project is not finished
        /// </response>
        /// <response code="500"> Internal Server Error </response>
        [HttpPost("{projectId:int}/reviews")]
        public async Task<ActionResult<IEnumerable<Review>>> AddReview(
            int projectId, 
            [FromBody] Review review)
        {
            try
            {
                var project = await _context.Projects.FindAsync(projectId);

                if (project == null) return BadRequest("InvalidIndexError: Project with such Id does not exist");

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
