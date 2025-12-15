using Microsoft.AspNetCore.Mvc;
using Sicma.DTO.Request.Institution;
using Sicma.DTO.Response;
using Sicma.Service.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace Sicma.API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InstitutionController : ControllerBase
    {
        private readonly IInstitutionService _institutionService;

        public InstitutionController(IInstitutionService institutionService)
        {
            _institutionService = institutionService;
        }
        
        [HttpGet("GetInstitutions")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetInstitutions([FromQuery] InstitutionSearchRequest request,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _institutionService.GetAll(request);
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

        [HttpGet("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetInstitutionsById([FromQuery] Guid request,
            CancellationToken cancellationToken = default)
        {
            if (request == Guid.Empty)
                return BadRequest(ModelState);

            try
            {
                var result = await _institutionService.GetById(request);
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
        public async Task<IActionResult> CreateInstitution([FromBody]InstitutionRequest institutionRequest,
            CancellationToken cancellationToken = default)
        {
            var userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if(institutionRequest == null)
                return BadRequest(ModelState);

            BaseResponse result = await _institutionService.Create(institutionRequest,Guid.Parse(userId!));
            if (result.Success)
            {
                return Created();
            }
            else
            {
                return BadRequest(result.Message);
            }
        }


        [HttpDelete("{id:guid}", Name = "DeleteInstitution")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteInstitution(Guid institutionId)
        {
            var userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            if (institutionId == Guid.Empty)
                return BadRequest(ModelState);

            BaseResponse result = await _institutionService.Delete(institutionId);

            if (result.Success)
            {
                return Ok(result.Message);
            }
            else { 
                return BadRequest(result.Message);
            }
        }

        [HttpPut("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateInstitution(Guid id,[FromBody] InstitutionRequest institutionRequest,
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (institutionRequest == null)
                return BadRequest(ModelState);

            var userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            BaseResponse result = await _institutionService.Update(id,institutionRequest);
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
