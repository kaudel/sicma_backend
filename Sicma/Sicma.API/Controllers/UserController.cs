using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sicma.DTO.Request.Token;
using Sicma.DTO.Request.User;
using Sicma.DTO.Response;
using Sicma.DTO.Response.Users;
using Sicma.Service.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace Sicma.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenHistoryService _tokenHistoryService;

        public ICollection<ListUsersResponse> listUsers { get; set; } = new List<ListUsersResponse>();
        public UserSearchRequest request { get; set; } = new UserSearchRequest();

        public UserController(IUserService userService, ITokenHistoryService tokenHistoryService)
        {
            _userService = userService;
            _tokenHistoryService = tokenHistoryService;
        }

        [Authorize]
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
                    return BadRequest(result!.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Authorize]
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            try
            {
                var result = await _userService.GetById(id);

                if (result != null && result.Success)
                {
                    return Ok(result.Data);
                }
                else
                    return BadRequest(result!.Message);
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

        [Authorize]
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserRequest request)
        {
            var result = await _userService.Update(id, request);

            if (result != null && result.Success)
            {
                return Ok(result.Message);
            }
            else
                return BadRequest(result!.Message);
        }

        [Authorize]
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var result = await _userService.Delete(id);

            if (result != null && result.Success)
            {
                return Ok(result.Message);
            }
            else
                return BadRequest(result!.Message);

        }

        [AllowAnonymous]
        [HttpPost("Login")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest userLogin)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (userLogin == null)
                return BadRequest(ModelState);

            var loginResponse = await _userService.Authenticate(userLogin);

            if (loginResponse == null || !loginResponse.Success)
                return Unauthorized(loginResponse!.Message);

            var accessToken = await _tokenHistoryService.CreateAccessToken(loginResponse.Data!);

            if (accessToken == null || !accessToken.Success)
            {
                return BadRequest(accessToken!.Message);
            }

            var tokenRefreshRequest = new TokenRefreshRequest()
            {
                ExpiredToken = accessToken.Data!.Token,
            };

            var refreshToken = await _tokenHistoryService.CreateRefreshToken(tokenRefreshRequest, loginResponse.Data!.Id);

            if (refreshToken == null || !refreshToken.Success)
            {
                return BadRequest(refreshToken!.Message);
            }

            var response = new UserLoginResponse()
            {
                Token = accessToken.Data.Token,
                RefreshToken = refreshToken.Data!.RefreshToken
            };

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("Refresh")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Refresh([FromBody] TokenRefreshRequest tokenRefreshRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (tokenRefreshRequest == null)
                return BadRequest(ModelState);

            var tokenhandler = new JwtSecurityTokenHandler();
            var token = tokenhandler.ReadJwtToken(tokenRefreshRequest.ExpiredToken);

            if (token.ValidTo > DateTime.UtcNow)
                return Unauthorized("Refresh Token invalid");

            UserAutenticateResponse userAuth = new()
            {
                Id = Guid.Parse(token.Subject)
            };

            //validate refreshtoken
            var tokenRefreshExists = await _tokenHistoryService.ExistsTokenHistory(tokenRefreshRequest, Guid.Parse(token.Subject));

            if (tokenRefreshExists == null || !tokenRefreshExists.Success)
                return Unauthorized("Refresh Token invalid");

            var accessToken = await _tokenHistoryService.CreateAccessToken(userAuth);

            if (accessToken == null || !accessToken.Success)
            {
                return BadRequest(accessToken!.Message);
            }

            var tokenRefreshRequestNew = new TokenRefreshRequest()
            {
                ExpiredToken = accessToken.Data!.Token
            };

            var refreshToken = await _tokenHistoryService.CreateRefreshToken(tokenRefreshRequestNew, Guid.Parse(token.Subject));

            if (refreshToken == null || !refreshToken.Success)
            {
                return BadRequest(refreshToken!.Message);
            }

            var response = new UserLoginResponse()
            {
                Token = accessToken.Data.Token,
                RefreshToken = refreshToken.Data!.RefreshToken
            };

            return Ok(response);

        }

        [AllowAnonymous]
        [HttpPost("Logout")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Logout([FromBody] TokenRefreshRequest tokenRefreshRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (tokenRefreshRequest == null)
                return BadRequest(ModelState);

            var tokenhandler = new JwtSecurityTokenHandler();
            var token = tokenhandler.ReadJwtToken(tokenRefreshRequest.ExpiredToken);
            Guid userId = Guid.Parse(token.Subject);

            UserAutenticateResponse userAuth = new()
            {
                Id = userId
            };

            //validate refreshtoken
            var tokenRefreshExists = await _tokenHistoryService.ExistsTokenHistory(tokenRefreshRequest,userId);

            if (tokenRefreshExists == null || !tokenRefreshExists.Success)
                return Unauthorized("Refresh Token invalid");

            var isLogout = await _tokenHistoryService.InvalidateToken(tokenRefreshRequest, userId);

            if (isLogout == null || !isLogout.Success)
            {
                return BadRequest(isLogout!.Message);
            }

            return Ok(isLogout.Message);
        }
    }
}
