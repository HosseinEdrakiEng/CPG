using Microsoft.AspNetCore.Mvc;

namespace CPG.Api.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class CreditLoanController : ControllerBase
    {
    }
}
