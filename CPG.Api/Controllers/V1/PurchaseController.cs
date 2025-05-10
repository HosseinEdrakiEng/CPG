using Application.Abstraction.IService;
using Application.Models;
using CPG.Api.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CPG.Api.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;

        public PurchaseController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreatePurchaseRequestDto request, CancellationToken cancellationToken)
        {
            var model = request.Adapt<CreatePurchaseRequestModel>();
            var response = await _purchaseService.Create(model, cancellationToken);
            return StatusCode((int)response.Error.StatusCode, response);
        }

        [HttpGet("inquiry/{trackingCode}")]
        public async Task<IActionResult> Inquiry([FromRoute, Required] string trackingCode, CancellationToken cancellationToken)
        {
            var response = await _purchaseService.Inquiry(trackingCode, cancellationToken);
            return StatusCode((int)response.Error.StatusCode, response);
        }
    }
}
