using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace VolumeMixer
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            DisplayAlert("asdf", "tapped", "bye");
        }

        void DragGestureRecognizer_DragStarting(System.Object sender, Xamarin.Forms.DragStartingEventArgs e)
        {
            // allow it to move up/down along the track
            BoxView thumb = (BoxView)sender;

            
        }
    }
}
