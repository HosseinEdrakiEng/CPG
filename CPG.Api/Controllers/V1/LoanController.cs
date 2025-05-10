using Application.Abstraction.IService;
using Application.Models;
using CPG.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CPG.Api.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _loanService;

        public LoanController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        [HttpPost("consume")]
        public async Task<IActionResult> Consume(ConsumeLoanRequestDto request, CancellationToken cancellationToken)
        {
            var model = new ConsumeLoanRequestModel
            {
                PurchaseDetailId = request.PurchaseDetailId,
                TrackingCode = request.TrackingCode,
                NationalCode = Request.HttpContext.User?.Claims?.Where(r => r.Type == "nationalCode")?.FirstOrDefault()?.Value,
                Phonenumber = Request.HttpContext.User?.Claims?.Where(r => r.Type == "phoneNumber")?.FirstOrDefault()?.Value
            };
            var response = await _loanService.Consume(model, cancellationToken);
            return StatusCode((int)response.Error.StatusCode, response);
        }

        [HttpGet("balance")]
        public async Task<IActionResult> Balance(CancellationToken cancellationToken)
        {
            var model = new BalanceLoanRequestModel
            {
                NationalCode = Request.HttpContext.User?.Claims?.Where(r => r.Type == "nationalCode")?.FirstOrDefault()?.Value,
                Phonenumber = Request.HttpContext.User?.Claims?.Where(r => r.Type == "phoneNumber")?.FirstOrDefault()?.Value
            };
            var response = await _loanService.Balance(model, cancellationToken);
            return StatusCode((int)response.Error.StatusCode, response);
        }
    }
}
