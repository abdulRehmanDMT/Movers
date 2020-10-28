using System;
using System.Collections.Generic;
using Movers.Views;
using Xamarin.Forms;

namespace Movers
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(RevisionNewEstimate), typeof(RevisionNewEstimate));
            Routing.RegisterRoute(nameof(RevisionOpts), typeof(RevisionOpts));

            Shell.SetTabBarIsVisible(this, false);
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
