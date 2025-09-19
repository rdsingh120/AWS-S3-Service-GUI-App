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
            if (BucketsDataGrid.SelectedItem is not Amazon.S3.Model.S3Bucket selectedBucket)
            {
                MessageBox.Show("Please select a bucket first.");
                return;
            }

            if(await BucketOperations.IsBucketEmpty(selectedBucket.BucketName))
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
                MessageBoxResult result = MessageBox.Show(
                    "Bucket is not empty, still want to delete it?",
                    $"{selectedBucket.BucketName} contains files",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes) 
                {
                    MessageBox.Show("Worked");
                }
            }
        }
    }
}
