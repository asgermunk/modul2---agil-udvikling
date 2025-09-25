using Microsoft.AspNetCore.Mvc;

namespace modul2_agiludvikling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Gets all users
        /// </summary>
        /// <returns>List of all users</returns>
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }

        /// <summary>
        /// Gets a specific user by ID
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>User if found, otherwise NotFound</returns>
        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }
            return Ok(user);
        }

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="user">User to create</param>
        /// <returns>Created user</returns>
        [HttpPost]
        public ActionResult<User> CreateUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("User data is required.");
            }

            if (string.IsNullOrWhiteSpace(user.Name) || string.IsNullOrWhiteSpace(user.Email))
            {
                return BadRequest("Name and Email are required fields.");
            }

            var createdUser = _userService.CreateUser(user);
            return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
        }

        /// <summary>
        /// Updates an existing user
        /// </summary>
        /// <param name="id">User ID to update</param>
        /// <param name="user">Updated user data</param>
        /// <returns>NoContent if successful, NotFound if user doesn't exist</returns>
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("User data is required.");
            }

            if (string.IsNullOrWhiteSpace(user.Name) || string.IsNullOrWhiteSpace(user.Email))
            {
                return BadRequest("Name and Email are required fields.");
            }

            var updated = _userService.UpdateUser(id, user);
            if (!updated)
            {
                return NotFound($"User with ID {id} not found.");
            }

            return NoContent();
        }

        /// <summary>
        /// Deletes a user
        /// </summary>
        /// <param name="id">User ID to delete</param>
        /// <returns>NoContent if successful, NotFound if user doesn't exist</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var deleted = _userService.DeleteUser(id);
            if (!deleted)
            {
                return NotFound($"User with ID {id} not found.");
            }

            return NoContent();
        }
    }
}