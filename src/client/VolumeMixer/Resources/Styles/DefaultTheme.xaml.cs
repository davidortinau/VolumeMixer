using System;
using System.Collections.Generic;

using Xamarin.Forms;

[assembly: ExportFont("fa-solid-900.ttf", Alias = "FontAwesome")]
[assembly: ExportFont("OpenSans-Regular.ttf", Alias = "OpenSansRegular")]
[assembly: ExportFont("OpenSans-SemiBold.ttf", Alias = "OpenSansSemiBold")]
namespace VolumeMixer.Resources.Styles
{
    public partial class DefaultTheme
    {
        public DefaultTheme()
        {
            InitializeComponent();
        }
    }
}
