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

        [HttpPost]
        //[ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserRequest request)
        {
            var response = await _userService.Register(request);
            return response.Success ? Ok(response) : BadRequest(response.Message);
        }

        [HttpGet("GetAll")]
        //[ProducesResponseType(typeof(List<>), StatusCodes.Status200OK)]
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

        [HttpPut("{id}")]
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById( string id)
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
            } catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
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
