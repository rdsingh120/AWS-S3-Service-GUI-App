using System.Windows;

namespace AWSS3Service
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnBucketOps_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnObjectOps_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}