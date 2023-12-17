using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DbREstService.Data;
using DbREstService.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace DbREstService.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ProjectDbContext _context;

        public TasksController(ProjectDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Task>>> GetTasks(
            [FromQuery(Name = "projectId")] int projectId)
        {
            try
            {
                if(!await  _context.Projects.AnyAsync(p => p.Id == projectId))
                {
                    return BadRequest("Task with such Id does not exist");
                }

                return await _context.Tasks
                    .Where(t => t.ProjectId == projectId)
                    .Include(t => t.Project)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("{taskId:int}")]
        public async Task<ActionResult<Models.Task>> GetTask(int taskId)
        {
            try
            {
                var task = await _context.Tasks
                    .Include(t => t.Project)
                    .FirstAsync(t => t.Id == taskId);

                if (task == null) return BadRequest("Task with such Id does not exist");

                return task;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPatch("{taskId:int}")]
        public async Task<ActionResult<Models.Task>> PatchTask(
            int taskId,
            [FromBody] JsonPatchDocument<Models.Task> patchDoc)
        {
            var task = await _context.Tasks.FindAsync(taskId);

            if (task == null)
            {
                return BadRequest("Task with such Id does not exist");
            }

            patchDoc.ApplyTo(task, ModelState);
            _context.SaveChanges();

            return RedirectToAction("GetTask", new { taskId });
        }

        [HttpPost]
        public async Task<ActionResult<Models.Task>> CreateTask([FromBody] Models.Task task)
        {
            try
            {
                _context.Tasks.Add(task);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetTask", new { taskId = task.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpDelete("{taskId:int}")]
        public async Task<ActionResult<Models.Task>> DeleteTask(int taskId)
        {
            try
            {
                var task = await _context.Tasks.FindAsync(taskId);
                if (task == null)
                {
                    return NotFound();
                }

                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();

                return task;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
