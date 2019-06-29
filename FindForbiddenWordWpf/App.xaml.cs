using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace FindForbiddenWordWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static bool isCreatedNew;
        private Mutex mtx = new Mutex(false, "MyMutex", out isCreatedNew);
        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                if (!isCreatedNew)
                {
                    MessageBox.Show("Application is already running");
                    App.Current.Shutdown();
                }
                else
                {
                    base.OnStartup(e);
                }
            }
            catch (Exception)
            {
            }

        }
        protected override void OnExit(ExitEventArgs e)
        {
            try
            {
                if (!isCreatedNew)
                    mtx.ReleaseMutex();
                base.OnExit(e);
            }
            catch (Exception)
            {

            }

        }
    }
}
