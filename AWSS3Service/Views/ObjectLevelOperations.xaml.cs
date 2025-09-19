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

        private async void Window_Loaded(object sender, RoutedEventArgs e) 
        {
            await LoadBuckets();
        }

        private async void BucketComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if(BucketComboBox.SelectedItem is Amazon.S3.Model.S3Bucket bucket)
            {
                var objects = await ObjectOperations.GetObjectList(bucket.BucketName);
                ObjectDataGrid.ItemsSource = objects;
            }
        }

        private void DownloadButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
