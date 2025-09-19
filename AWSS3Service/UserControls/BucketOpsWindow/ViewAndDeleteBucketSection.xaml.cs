using AWSS3Service.Services;
using System.Windows;
using System.Windows.Controls;

namespace AWSS3Service.UserControls
{
    public partial class ViewAndDeleteBucketSection : UserControl
    {
        public ViewAndDeleteBucketSection()
        {
            InitializeComponent();
        }
        public async Task LoadBuckets()
        {
            var buckets = await BucketOperations.GetBucketList();
            BucketsDataGrid.ItemsSource = buckets;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadBuckets();
        }

        private async void DeleteBucketButton_Click(object sender, RoutedEventArgs e)
        {
            if (BucketsDataGrid.SelectedItem is not Amazon.S3.Model.S3Bucket selectedBucket)
            {
                MessageBox.Show("Please select a bucket first.");
                return;
            }

            if (await BucketOperations.IsBucketEmpty(selectedBucket.BucketName))
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
                    bool isAllFilesDeleted = await BucketOperations.DeleteAllFiles(selectedBucket.BucketName);

                    if (isAllFilesDeleted)
                    {
                        await BucketOperations.DeleteBucket(selectedBucket.BucketName);
                        MessageBox.Show($"Bucket {selectedBucket.BucketName} deleted.");
                        await LoadBuckets();
                    }
                }
            }
        }
    }
}
