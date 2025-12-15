using Microsoft.AspNetCore.Mvc;
using Sicma.DTO.Request.UserRecord;
using Sicma.DTO.Response;
using Sicma.Service.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace Sicma.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRecordController : ControllerBase
    {
        private readonly IUserRecordService _service;

        public UserRecordController(IUserRecordService service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAppRecords([FromQuery] UserRecordSearchRequest request,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _service.GetAll(request);
                if (result != null && result.Success)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result!.Message);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserRecordById([FromQuery] Guid id,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _service.GetById(id);
                if (result != null && result.Success)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result!.Message);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateUserRecord([FromBody] UserRecordRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (request == null)
                return BadRequest(ModelState);

            var userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            BaseResponse result = await _service.Create(request, Guid.Parse(userId));
            if (result.Success)
            {
                return Created();
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteUserRecord(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest(ModelState);

            BaseResponse result = await _service.Delete(id);

            if (result.Success)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateUserRecord(Guid id, [FromBody] UserRecordRequest request,
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (request == null)
                return BadRequest(ModelState);

            BaseResponse result = await _service.Update(id, request);
            if (result.Success)
            {
                return Created();
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

    }
}
