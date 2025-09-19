using Amazon;
using Amazon.S3;

namespace AWSS3Service.Services
{
    internal class S3ClientProvider
    {
        public readonly static IAmazonS3 s3Client;

        static S3ClientProvider()
        {
            s3Client = GetS3Client();
        }

        private static IAmazonS3? GetS3Client()
        {
            string? awsAccessKey = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID");
            string? awsSecretKey = Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY"); 

            if(string.IsNullOrEmpty(awsSecretKey) || string.IsNullOrEmpty(awsAccessKey)) {
                throw new InvalidOperationException("AWS credentials not found");
            }
            return new AmazonS3Client(awsAccessKey, awsSecretKey, RegionEndpoint.USEast1);
        }
    }
}
