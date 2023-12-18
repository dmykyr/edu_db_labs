using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DbREstService.Data;
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

        /// <summary>
        ///     Retrieves tasks related to specific project
        /// </summary>
        /// <response code="200"> List of tasks </response>
        /// <response code="400"> InvalidIndexError: Task with such Id does not exist </response>
        /// <response code="500"> Internal Server Error </response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Task>>> GetTasks(
            [FromQuery(Name = "projectId")] int projectId)
        {
            try
            {
                if(!await  _context.Projects.AnyAsync(p => p.Id == projectId))
                {
                    return BadRequest("InvalidIndexError: Task with such Id does not exist");
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

        /// <summary>
        ///     Retrieves specific task by unique id
        /// </summary>
        /// <response code="200"> Specific Task </response>
        /// <response code="400">  InvalidIndexError: Task with such Id does not exist </response>
        /// <response code="500"> Internal Server Error </response>
        [HttpGet("{taskId:int}")]
        public async Task<ActionResult<Models.Task>> GetTask(int taskId)
        {
            try
            {
                var task = await _context.Tasks
                    .Include(t => t.Project)
                    .FirstAsync(t => t.Id == taskId);

                if (task == null) return BadRequest("InvalidIndexError: Task with such Id does not exist");

                return task;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        /// <summary>
        ///     Update specific values for task by unique id
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
        /// <response code="200"> An updated Task </response>
        /// <response code="400"> InvalidIndexError: Task with such Id does not exist </response>
        /// <response code="500"> Internal Server Error </response>
        [HttpPatch("{taskId:int}")]
        public async Task<ActionResult<Models.Task>> PatchTask(
            int taskId,
            [FromBody] JsonPatchDocument<Models.Task> patchDoc)
        {
            var task = await _context.Tasks.FindAsync(taskId);
            if (task == null)
            {
                return BadRequest("InvalidIndexError: Task with such Id does not exist");
            }

            patchDoc.ApplyTo(task, ModelState);
            _context.SaveChanges();

            return RedirectToAction("GetTask", new { taskId });
        }

        /// <summary>
        ///     Add Task to db
        /// </summary>
        /// <response code="200"> A newly created Task </response>
        /// <response code="400"> InvalidIndexError: Task with such Id does not exist </response>
        /// <response code="500"> Internal Server Error </response>
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

        /// <summary>
        ///     Remove Task from db by unique id
        /// </summary>
        /// <response code="200"> Just removed Task </response>
        /// <response code="400"> InvalidIndexError: Task with such Id does not exist </response>
        /// <response code="500"> Internal Server Error </response>
        [HttpDelete("{taskId:int}")]
        public async Task<ActionResult<Models.Task>> DeleteTask(int taskId)
        {
            try
            {
                var task = await _context.Tasks.FindAsync(taskId);
                if (task == null)
                {
                    return BadRequest("InvalidIndexError: Task with such Id does not exist");
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
