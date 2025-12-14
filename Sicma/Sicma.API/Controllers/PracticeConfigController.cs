using Microsoft.AspNetCore.Mvc;
using Sicma.DTO.Request.PracticeConfig;
using Sicma.DTO.Response;
using Sicma.Service.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace Sicma.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PracticeConfigController : ControllerBase
    {
        public readonly IPracticeConfigService _pConfigService;

        public PracticeConfigController(IPracticeConfigService practiceConfigService)
        {
            _pConfigService = practiceConfigService;
        }

        [HttpGet("GetPracticeConfigs")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetPracticeConfigs([FromQuery] PracticeConfigSearchRequest request,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _pConfigService.GetAll(request);
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

        [HttpGet("GetPracticeConfigById")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetPracticeConfigById([FromQuery] string request,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _pConfigService.GetById(request);
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateInstitution([FromBody] PracticeConfigRequest practiceConfigRequest,
            CancellationToken cancellationToken = default)
        {
            var userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (practiceConfigRequest == null)
                return BadRequest(ModelState);

            BaseResponse result = await _pConfigService.Create(practiceConfigRequest, userId!);
            if (result.Success)
            {
                return Created();
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpDelete("practiceConfigId:string", Name = "DeletePracticeConfig")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePracticeConfig(string practiceConfigId)
        {
            var userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            if (string.IsNullOrEmpty(practiceConfigId))
                return BadRequest(ModelState);

            BaseResponse result = await _pConfigService.Delete(practiceConfigId);

            if (result.Success)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdatePracticeConfig(string id, [FromBody] PracticeConfigRequest practiceConfigRequest,
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (practiceConfigRequest == null)
                return BadRequest(ModelState);

            var userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            BaseResponse result = await _pConfigService.Update(id, practiceConfigRequest);
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
