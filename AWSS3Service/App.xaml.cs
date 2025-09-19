using DotNetEnv;
using System.Windows;

namespace AWSS3Service
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Env.Load();
            base.OnStartup(e);
        }
    }

}
