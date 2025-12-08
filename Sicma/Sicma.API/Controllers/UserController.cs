using Microsoft.AspNetCore.Mvc;
using Sicma.DTO.Request.User;
using Sicma.DTO.Response;
using Sicma.DTO.Response.Users;
using Sicma.Service.Interfaces;

namespace Sicma.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public ICollection<ListUsersResponse> listUsers { get; set; } = new List<ListUsersResponse>();
        public UserSearchRequest request { get; set; } = new UserSearchRequest();

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet("GetAll")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetAllUsers([FromQuery] UserSearchRequest request,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _userService.GetAll(request);
                if (result != null && result.Success)
                {
                    return Ok(result.Data);
                }
                else
                    return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetUserById(string id)
        {
            try
            {
                var result = await _userService.GetById(id);

                if (result != null && result.Success)
                {
                    return Ok(result.Data);
                }
                else
                    return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserRequest request,
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (request == null)
                return BadRequest(ModelState);


            var response = await _userService.Register(request);
            return response.Success ? Ok(response) : BadRequest(response.Message);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UserRequest request)
        {
            var result = await _userService.Update(id, request);

            if (result != null && result.Success)
            {
                return Ok(result.Message);
            }
            else
                return BadRequest(result.Message);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await _userService.Delete(id);

            if (result != null && result.Success)
            {
                return Ok(result.Message);
            }
            else
                return BadRequest(result.Message);

        }
    }
}
