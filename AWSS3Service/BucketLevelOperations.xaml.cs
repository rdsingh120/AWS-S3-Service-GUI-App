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
    }
}
