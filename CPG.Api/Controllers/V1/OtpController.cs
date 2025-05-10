using Application.Abstraction.IService;
using CPG.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace CPG.Api.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class OtpController : ControllerBase
    {
        private readonly IOtpService _otpService;

        public OtpController(IOtpService otpService)
        {
            _otpService = otpService;
        }

        [HttpPost("send")]
        [NonAction]
        public async Task<IActionResult> Send([FromBody] SendOtpRequestDto request, CancellationToken cancellationToken)
        {
            var response = await _otpService.Send(request.Phonenumnber, cancellationToken);
            return StatusCode((int)response.Error.StatusCode, response);
        }

        [HttpPost("verify")]
        [NonAction]
        public async Task<IActionResult> Verify([FromBody] VerifyOtpRequestDto request, CancellationToken cancellationToken)
        {
            var response = await _otpService.Verify(request.Phonenumnber, request.Token, request.Value, cancellationToken);
            return StatusCode((int)response.Error.StatusCode, response);
        }
    }
}
