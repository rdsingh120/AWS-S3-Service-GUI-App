using Amazon.S3;
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

        public static async Task<bool> IsBucketEmpty(string bucketName)
        {
            var request = new ListObjectsV2Request 
            { 
                BucketName = bucketName,
                MaxKeys = 1
            };

            var response = await S3ClientProvider.s3Client.ListObjectsV2Async(request);
            return response.KeyCount == 0;
        }

        public static async Task<bool> DeleteAllFiles(string bucketName)
        {
            var request = new ListObjectsV2Request
            {
                BucketName = bucketName
            };

            try
            {
                ListObjectsV2Response response;

                do
                {
                    response = await S3ClientProvider.s3Client.ListObjectsV2Async(request);
                    foreach (var obj in response.S3Objects)
                    {
                        await S3ClientProvider.s3Client.DeleteObjectAsync(bucketName, obj.Key);
                    }

                    request.ContinuationToken = response.NextContinuationToken;
                }
                while (response.IsTruncated == true);
                return true;
            }
            catch (AmazonS3Exception ex)
            {
                Console.WriteLine($"Error deleting objects: {ex.Message}");
                return false;
            }
        }
    }
}
