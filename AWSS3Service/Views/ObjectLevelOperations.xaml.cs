using AWSS3Service.Services;
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
    }
}
