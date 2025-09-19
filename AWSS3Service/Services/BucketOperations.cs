using Amazon.S3.Model;

namespace AWSS3Service.Services
{
    public static class BucketOperations
    {
        public static async Task CreateBucket(string bucketName)
        {
            var putBucketRequest = new PutBucketRequest 
            { 
                BucketName = bucketName,
                UseClientRegion = true,
            };

            await S3ClientProvider.s3Client.PutBucketAsync(putBucketRequest);
        }

        public static async Task DeleteBucket(string bucketName)
        {
            await S3ClientProvider.s3Client.DeleteBucketAsync(bucketName);
        }

        public static async Task<List<S3Bucket>> GetBucketList()
        {
            ListBucketsResponse response = await S3ClientProvider.s3Client.ListBucketsAsync();
            return response.Buckets;
        }
    }
}
