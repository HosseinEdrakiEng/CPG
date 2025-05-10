using Application.Abstraction.IService;
using Application.Models;
using CPG.Api.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CPG.Api.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class PurchaseDetailController : ControllerBase
    {
        private readonly IPurchaseDetailService _purchaseDetailService;

        public PurchaseDetailController(IPurchaseDetailService purchaseDetailService)
        {
            _purchaseDetailService = purchaseDetailService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreatePurchaseDetailRequestDto request, CancellationToken cancellationToken)
        {
            var model = request.Adapt<CreatePurchaseDetailRequestModel>();
            var response = await _purchaseDetailService.Create(model, cancellationToken);
            return StatusCode((int)response.Error.StatusCode, response);
        }
    }
}
