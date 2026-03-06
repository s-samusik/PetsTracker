namespace PT.Api.Models;

public record ListResponse<T>(int Count, IReadOnlyList<T> Data);