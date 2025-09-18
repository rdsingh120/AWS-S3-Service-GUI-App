using AWSS3Service.Services;
using System.Threading.Tasks;
using System.Windows;

namespace AWSS3Service
{
    public partial class BucketLevelOperations : Window
    {
        BucketOperations bucketOperations;
        public BucketLevelOperations()
        {
            InitializeComponent();
            this.bucketOperations = new BucketOperations();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void CreateBucketButton_Click(object sender, RoutedEventArgs e)
        {
            string bucketName = BucketNameTextBox.Text;

            if (string.IsNullOrWhiteSpace(bucketName)) 
            {
                MessageBox.Show("Please enter the bucket name");
            }
            else
            {
                try
                {
                    await bucketOperations.CreateBucket(bucketName);
                    MessageBox.Show($"Bucket {bucketName} created successfully.");
                }
                catch( Exception ex )
                {
                    MessageBox.Show($"Error creating bucket: {ex.Message}");
                }

                BucketNameTextBox.Text = string.Empty;
            }
        }
    }
}
