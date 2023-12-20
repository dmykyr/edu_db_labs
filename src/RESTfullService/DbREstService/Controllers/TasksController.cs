using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DbREstService.Data;
using Microsoft.AspNetCore.JsonPatch;
using DbREstService.Responses;
using DbREstService.DTOs;

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
        /// <response code="400"> InvalidIndexError: Project with such Id does not exist </response>
        /// <response code="500"> Internal Server Error </response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskResponse>>> GetTasks(
            [FromQuery(Name = "projectId")] int projectId)
        {
            try
            {
                var project = await _context.Projects
                    .Include(p => p.Tasks)
                    .FirstOrDefaultAsync(p => p.Id == projectId);
                if(project == null)
                {
                    return BadRequest("InvalidIndexError: Project with such Id does not exist");
                }

                return project.Tasks.Select(TaskResponse.ConvertFromModel).ToList();
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
        public async Task<ActionResult<TaskResponse>> GetTask(int taskId)
        {
            try
            {
                var task = await _context.Tasks.FirstAsync(t => t.Id == taskId);

                if (task == null) return BadRequest("InvalidIndexError: Task with such Id does not exist");

                return TaskResponse.ConvertFromModel(task);
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
        public async Task<ActionResult<TaskResponse>> PatchTask(
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
        /// <response code="400"> InvalidIndexError: Project with such Id does not exist </response>
        /// <response code="500"> Internal Server Error </response>
        [HttpPost]
        public async Task<ActionResult<TaskResponse>> CreateTask(
            [FromQuery] int projectId,
            [FromBody] TaskDTO taskDTO)
        {
            try
            {
                if(!await _context.Projects.AnyAsync(p => p.Id == projectId))
                {
                    return BadRequest("InvalidIndexError: Project with such Id does not exist");
                }

                var task = taskDTO.ToModel();
                task.ProjectId = projectId;

                var entityEntry = _context.Tasks.Add(task);
                await _context.SaveChangesAsync();

                return RedirectToAction("GetTask", new { taskId = entityEntry.Entity.Id });
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
        public async Task<ActionResult<TaskResponse>> DeleteTask(int taskId)
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

                return TaskResponse.ConvertFromModel(task);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
