using System;
using System.Collections.Generic;
using VolumeMixer.Pages;
using Xamarin.Forms;

namespace VolumeMixer
{
    public partial class AppShell
    {
        public AppShell()
        {
            InitializeComponent();

            RegisterRoutes();
        }

        private void RegisterRoutes()
        {
            Routing.RegisterRoute("settings", typeof(SettingsPage));
        }
    }
}
