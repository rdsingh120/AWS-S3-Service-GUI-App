using Amazon.S3.Model;

namespace AWSS3Service.Services
{
    internal class BucketOperations
    {
        public async Task CreateBucket(string bucketName)
        {
            var putBucketRequest = new PutBucketRequest 
            { 
                BucketName = bucketName,
                UseClientRegion = true,
            };

            await S3ClientProvider.s3Client.PutBucketAsync(putBucketRequest);
        }
    }
}
