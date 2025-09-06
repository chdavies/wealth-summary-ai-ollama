using WealthSummary.Api.Application.Dtos;
using WealthSummary.Domain.Model;

namespace WealthSummary.Api.Application.Mappers
{
    public static class ClientMapper
    {
        public static ClientResponse Map(Client client)
        {
            return new ClientResponse(
                client.ClientId,
                client.FullName,
                client.DateOfBirth
            );
        }
    }
}
