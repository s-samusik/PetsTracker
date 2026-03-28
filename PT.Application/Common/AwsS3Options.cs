namespace PT.Application.Common;

public sealed class AwsS3Options
{
    public const string Name = "AWS";

    public string AccessKey { get; set; } = default!;
    public string SecretKey { get; set; } = default!;
    public string Bucket { get; set; } = default!;
    public string Endpoint { get; set; } = default!;
    public string PublicEndpoint { get; set; } = default!;
}