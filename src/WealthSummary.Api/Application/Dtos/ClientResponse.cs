namespace WealthSummary.Api.Application.Dtos;

public record ClientResponse(
    int ClientId, 
    string Name, 
    DateTime DateOfBirth);
