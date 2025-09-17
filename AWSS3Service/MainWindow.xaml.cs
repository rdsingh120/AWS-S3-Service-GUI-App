using System.Windows;

namespace AWSS3Service
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BucketOpsButton_Click(object sender, RoutedEventArgs e)
        {
            BucketLevelOperations bucketLevelOperations = new BucketLevelOperations();
            bucketLevelOperations.ShowDialog();
        }

        private void ObjectOpsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}