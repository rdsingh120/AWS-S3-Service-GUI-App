using AWSS3Service.Services;
using System.Windows;
using System.Windows.Controls;

namespace AWSS3Service.UserControls
{
    public partial class CreateBucketSection : UserControl
    {
        public event Func<Task> BucketCreated;
        public CreateBucketSection()
        {
            InitializeComponent();
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
                    BucketNameTextBox.Text = string.Empty;

                    if (BucketCreated != null) await BucketCreated.Invoke();
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error creating bucket: {ex.Message}");
                }                                
            }
        }
    }
}
