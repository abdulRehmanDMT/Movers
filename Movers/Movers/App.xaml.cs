using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Movers.Views;
using Syncfusion.Licensing;

namespace Movers
{
    public partial class App : Application
    {

        public App()
        {
            SyncfusionLicenseProvider.RegisterLicense("MzExMjM0QDMxMzgyZTMyMmUzMGJNZUF1RytQcUFUbFJuWEhoMWlLWi96eTh5U25IVEpuRVJWMWNtbGozU0E9");
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
