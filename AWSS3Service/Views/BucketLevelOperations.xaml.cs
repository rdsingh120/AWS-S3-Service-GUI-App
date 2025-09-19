using AWSS3Service.Services;
using System.Windows;

namespace AWSS3Service
{
    public partial class BucketLevelOperations : Window
    {
        public BucketLevelOperations()
        {
            InitializeComponent();
            CreateBucketSectionControl.BucketCreated += ViewAndDeleteSectionControl.LoadBuckets;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        
    }
}
