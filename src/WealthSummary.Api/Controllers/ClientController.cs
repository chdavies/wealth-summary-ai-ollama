using Microsoft.AspNetCore.Mvc;
using WealthSummary.Api.Application.Dtos;
using WealthSummary.Api.Application.Mappers;
using WealthSummary.Domain.Model;
using WealthSummary.Domain.Repositories;

namespace WealthSummary.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;

        public ClientController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        [HttpGet("{clientId}")]
        public async Task<ActionResult<Client>> GetClient(int clientId, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetByIdAsync(clientId);
            if (client == null)
            {
                return NotFound();
            }
            return Ok(client);
}
    }
}
