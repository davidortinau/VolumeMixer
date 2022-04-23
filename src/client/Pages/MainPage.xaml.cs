using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mqtt;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Views;

namespace VolumeMixer.Pages;

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

    void ConnectBtn_Clicked(System.Object sender, System.EventArgs e)
    {
        
        this.ShowPopup(new Popup
        {
            Content = new VerticalStackLayout
            {
                Children =
                    {
                        new Label
                        {
                            Text = "This is a very important message!"
                        }
                    }
            }
        });
    }




}