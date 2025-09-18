using Amazon;
using Amazon.S3;

namespace AWSS3Service.Service
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
            string awsAccessKey = "";
            string awsSecretKey = "";
            return new AmazonS3Client(awsAccessKey, awsSecretKey, RegionEndpoint.USEast1);
        }
    }
}
