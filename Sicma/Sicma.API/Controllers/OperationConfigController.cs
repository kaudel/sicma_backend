using Microsoft.AspNetCore.Mvc;
using Sicma.DTO.Request.OperationConfig;
using Sicma.DTO.Response;
using Sicma.Service.Interfaces;

namespace Sicma.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationConfigController : ControllerBase
    {
        private readonly IOperationConfigService _operationConfigService;

        public OperationConfigController(IOperationConfigService operationConfigService)
        {
            _operationConfigService = operationConfigService;
        }

        [HttpGet("GetOperationConfigs")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOperationConfigs([FromQuery] OperationConfigSearchRequest request,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _operationConfigService.GetAll(request);
                if (result != null && result.Success)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result.Message);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("GetOperationConfigsById")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOperationConfigsById([FromQuery] string requestId,
           CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _operationConfigService.GetById(requestId);
                if (result != null && result.Success)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result.Message);
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
        public async Task<IActionResult> CreateOperationConfig([FromBody] OperationConfigRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (request == null)
                return BadRequest(ModelState);

            BaseResponse result = await _operationConfigService.Create(request);
            if (result.Success)
            {
                return Created();
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpDelete("operationConfigId:string", Name = "DeleteOperationConfig")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteOperationConfig(string operationConfigId)
        {
            if (string.IsNullOrEmpty(operationConfigId))
                return BadRequest(ModelState);

            BaseResponse result = await _operationConfigService.Delete(operationConfigId);

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
        public async Task<IActionResult> UpdateOperationConfig(string id, [FromBody] OperationConfigRequest request,
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (request == null)
                return BadRequest(ModelState);

            BaseResponse result = await _operationConfigService.Update(id, request);
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
