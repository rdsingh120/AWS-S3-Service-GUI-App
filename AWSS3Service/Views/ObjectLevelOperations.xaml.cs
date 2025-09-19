using AWSS3Service.Services;
using System.Threading.Tasks;
using System.Windows;

namespace AWSS3Service
{
    public partial class ObjectLevelOperations : Window
    {
        public ObjectLevelOperations()
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
            BucketComboBox.ItemsSource = buckets;
        }

        private async Task LoadFiles(Amazon.S3.Model.S3Bucket bucket)
        {
            if (bucket == null) return;
            var objects = await ObjectOperations.GetObjectList(bucket.BucketName);
            ObjectDataGrid.ItemsSource = objects;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e) 
        {
            await LoadBuckets();
        }

        private async void BucketComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if(BucketComboBox.SelectedItem is Amazon.S3.Model.S3Bucket bucket)
            {
                await LoadFiles(bucket);
            }
        }

        private async void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            if (BucketComboBox.SelectedItem is not Amazon.S3.Model.S3Bucket selectedBucket)
            {
                MessageBox.Show("Please select the bucket first");
                return;
            }

            if (ObjectDataGrid.SelectedItem is not Amazon.S3.Model.S3Object selectedObject)
            {
                MessageBox.Show("Please select the file first");
                return;
            }

            String fileName = selectedObject.Key;

            var saveAsDialog = new Microsoft.Win32.SaveFileDialog
            {
                FileName = fileName,
                DefaultExt = System.IO.Path.GetExtension(fileName),
                Filter = "All files (*.*)|*.*"
            };

            bool? result = saveAsDialog.ShowDialog();

            if (result == true)
            {
                try
                {
                    await ObjectOperations.DownloadFile(selectedBucket.BucketName, fileName, saveAsDialog.FileName);
                    MessageBox.Show("File download successful");

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Download failed: {ex.Message}");
                }
            }
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (BucketComboBox.SelectedItem is not Amazon.S3.Model.S3Bucket selectedBucket)
            {
                MessageBox.Show("Please select the bucket first");
                return;
            }

            if (ObjectDataGrid.SelectedItem is not Amazon.S3.Model.S3Object selectedObject)
            {
                MessageBox.Show("Please select the file first");
                return;
            }
            
            try
            {
                await ObjectOperations.DeleteFile(selectedBucket.BucketName, selectedObject.Key);
                MessageBox.Show("File deleted successfully");
                await LoadFiles(selectedBucket);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Deletion failed {ex.Message}");
            }
        }
    }
}
