using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DbREstService.Data;
using DbREstService.Models;
using Microsoft.AspNetCore.JsonPatch;
using DbREstService.Responses;

namespace DbREstService.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ProjectDbContext _context;

        public UsersController(ProjectDbContext context)
        {
            _context = context;
        }

        /// <summary>
        ///     Retrieves all users related to a specific Project
        /// </summary>
        /// <response code="200"> Users list </response>
        /// <response code="400"> InvalidIndexError: Project with such Id does not exist </response>
        /// <response code="500"> Internal Server Error </response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetUsers([FromQuery] int projectId)
        {
            try
            {
                var project = await _context.Projects.FindAsync(projectId);

                if (project == null)
                {
                    return BadRequest($"InvalidIndexError: Project with such Id does not exist");
                }

                return (await GetProjectUsers(project.Id)).Select(UserResponse.ConvertFromModel).ToList();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        /// <summary>
        ///     Retrieves specific User by unique Id
        /// </summary>
        /// <response code="200"> Specific User </response>
        /// <response code="400"> InvalidIndexError: User with such Id does not exist </response>
        /// <response code="500"> Internal Server Error </response>
        [HttpGet("{userId}")]
        public async Task<ActionResult<UserResponse>> GetUser(int userId)
        {
            try
            {
                var user = await _context.Users.FindAsync(userId);

                if (user == null)
                {
                    return BadRequest($"InvalidIndexError: User with such Id does not exist");
                }

                return UserResponse.ConvertFromModel(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        /// <summary>
        ///     Update specific values for user by unique id
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
        /// <response code="200"> An updated User </response>
        /// <response code="400"> InvalidIndexError: User with such Id does not exist </response>
        /// <response code="500"> Internal Server Error </response>
        [HttpPatch("{userId:int}")]
        public async Task<ActionResult<UserResponse>> PatchUser(
            int userId,
            [FromBody] JsonPatchDocument<User> patchDoc)
        {
            try
            {
                var user = await _context.Users.FindAsync(userId);

                if (user == null)
                {
                    return BadRequest("InvalidIndexError: User with such Id does not exist");
                }

                patchDoc.ApplyTo(user, ModelState);
                await _context.SaveChangesAsync();

                return RedirectToAction("GetUser", new { userId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        /// <summary>
        ///     Connect user to specific project
        /// </summary>
        /// <response code="201"> User connected successfully. Member Id: {memberId} </response>
        /// <response code="400">
        ///     InvalidIndexError: User with such Id does not exist&#xA;
        ///     InvalidIndexError: Project with such name does not exist&#xA;
        ///     DuplicateProjectMember: User with such Id already connected to selected project&#xA;
        ///     InvalidNameError: Role with such name does not exist
        /// </response>
        /// <response code="500"> Internal Server Error </response>
        [HttpPost("{userId:int}/projects/{projectId:int}")]
        public async Task<IActionResult> PostUser(
            [FromQuery] string roleName,
            int userId,
            int projectId)
        {
            try
            {
                var user = await _context.Users.FindAsync(userId);
                if (user == null)
                {
                    return BadRequest("InvalidIndexError: User with such Id does not exist");
                }

                var role = await _context.Roles.FirstAsync(r => r.Name == roleName);
                if (role == null)
                {
                    return BadRequest("InvalidNameError: Role with such name does not exist");
                }

                var project = await _context.Projects.FindAsync(projectId);
                if (project == null)
                {
                    return BadRequest("InvalidIndexError: Project with such name does not exist");
                }

                var projectUsers = await GetProjectUsers(projectId);
                if (projectUsers.Any(pu => pu.Members.Any(m => m.RoleId == role.Id))) 
                {
                    return BadRequest("DuplicateProjectMember: User with such Id already connected to selected project");
                }

                var addedEntityEntry = await _context.Members
                    .AddAsync(new Member() { RoleId = role.Id, UserId = userId });
                var member = addedEntityEntry.Entity;

                await _context.SaveChangesAsync();


                return Created($"User connected successfully. Member Id: {member.Id}", member);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        /// <summary>
        ///     Remove User from db by unique id
        /// </summary>
        /// <response code="200"> Just removed User </response>
        /// <response code="400"> InvalidIndexError: User with such Id does not exist </response>
        /// <response code="500"> Internal Server Error </response>
        [HttpDelete("{userId:int}")]
        public async Task<ActionResult<UserResponse>> DeleteUser(int userId)
        {
            try
            {
                var user = await _context.Users.FindAsync(userId);

                if (user == null)
                {
                    return BadRequest("InvalidIndexError: User with such Id does not exist");
                }

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                return UserResponse.ConvertFromModel(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        private async Task<List<User>> GetProjectUsers(int projectId)
        {
            return await _context.Users
                .Include(u => u.Members)
                .Where(u => _context.ProjectsMembers
                    .Where(pm => pm.ProjectId == projectId)
                    .Any(pm => u.Members.Any(m => m.Id == pm.MemberId)))
                .ToListAsync();
        }
    }
}