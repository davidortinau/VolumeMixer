using System;
using System.Net.Mqtt;
using VolumeMixer.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VolumeMixer
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            Hermes = new MessengerService();
        }

        public MessengerService Hermes { get; set; }  
    }
}
