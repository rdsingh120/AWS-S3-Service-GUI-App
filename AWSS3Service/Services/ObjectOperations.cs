using Amazon.S3.Model;

namespace AWSS3Service.Services
{
    public static class ObjectOperations
    {
        public static async Task<List<S3Object>> GetObjectList(string bucketName)
        {
            var request = new ListObjectsV2Request
            {
                BucketName = bucketName
            };

            var response = await S3ClientProvider.s3Client.ListObjectsV2Async(request);

            return response.S3Objects;
        }
    }
}
