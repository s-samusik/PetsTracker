using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Options;
using PT.Application.Common;
using PT.Application.Interfaces.Services;

namespace PT.Application.Services;

public sealed class S3HosterStorageService(
    IAmazonS3 s3, IOptions<AwsS3Options> options) : IFileStorageService
{
    private readonly IAmazonS3 _s3 = s3;
    private readonly AwsS3Options _options = options.Value;

    public async Task<string> UploadAsync(
        Stream stream,
        string fileName,
        string contentType,
        CancellationToken ct = default)
    {
        stream.Position = 0;

        var request = new PutObjectRequest
        {
            BucketName = _options.Bucket,
            Key = fileName,
            InputStream = stream,
            ContentType = contentType
        };

        await _s3.PutObjectAsync(request, ct);

        return $"{_options.Endpoint}/{_options.Bucket}/{fileName}";
    }
}
