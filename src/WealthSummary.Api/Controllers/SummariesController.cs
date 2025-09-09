using Microsoft.AspNetCore.Mvc;
using WealthSummary.Api.Application.Dtos;
using WealthSummary.Api.Application.Services.Contracts;

namespace WealthSummary.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SummariesController : ControllerBase
    {
        private readonly ISummaryService _summaryService;

        public SummariesController(ISummaryService summaryService)
        {
            _summaryService = summaryService;
        }

        [HttpGet("{clientId}")]
        public async Task<ActionResult<ClientSummaryResponse>> GetSummary(int clientId, CancellationToken ct)
        {
            var result = await _summaryService.BuildSummaryAsync(clientId, ct);
            if (result is null)
                return NotFound();

            return Ok(result);
        }
    }
}
