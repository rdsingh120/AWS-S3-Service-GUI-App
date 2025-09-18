using AWSS3Service.Services;
using System.Threading.Tasks;
using System.Windows;

namespace AWSS3Service
{
    public partial class BucketLevelOperations : Window
    {
        public BucketLevelOperations()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async Task LoadBuckets()
        {
            var buckets = await BucketOperations.GetBucketList();
            BucketsDataGrid.ItemsSource = buckets;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadBuckets();
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
                    await BucketOperations.CreateBucket(bucketName);
                    MessageBox.Show($"Bucket {bucketName} created successfully.");
                    await LoadBuckets();
                }
                catch( Exception ex )
                {
                    MessageBox.Show($"Error creating bucket: {ex.Message}");
                }

                BucketNameTextBox.Text = string.Empty;
            }
        }

        

        private async void DeleteBucketButton_Click(object sender, RoutedEventArgs e)
        {
            if (BucketsDataGrid.SelectedItem is Amazon.S3.Model.S3Bucket selectedBucket)
            {
                try
                {
                    await BucketOperations.DeleteBucket(selectedBucket.BucketName);
                    MessageBox.Show($"Bucket {selectedBucket.BucketName} deleted.");
                    await LoadBuckets();
                }
                catch (Exception ex) 
                {
                    MessageBox.Show($"Error deleting bucket: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please select a bucket first.");
            }
        }
    }
}
