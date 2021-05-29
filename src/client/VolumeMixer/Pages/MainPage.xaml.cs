using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace VolumeMixer.Pages
{
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            Connect();
        }

        private void Connect()
        {
            
        }
    }
}
