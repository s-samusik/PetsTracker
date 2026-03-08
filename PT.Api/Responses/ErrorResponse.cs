namespace PT.Api.Responses;

public sealed record ErrorResponse(string Error, string? Details = null);

