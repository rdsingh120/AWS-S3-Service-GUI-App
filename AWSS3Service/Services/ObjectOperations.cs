using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.Win32;

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

        public static async Task DownloadFile(string bucketName, string fileName, string savePath)
        {
            var downloadRequest = new TransferUtilityDownloadRequest
            {
                BucketName = bucketName,
                Key = fileName,
                FilePath = savePath
            };

            using var transferUtility = new TransferUtility(S3ClientProvider.s3Client);
            await transferUtility.DownloadAsync(downloadRequest);
        }
    }
}
