using VolumeMixer.Pages;
using VolumeMixer.Services;

namespace VolumeMixer;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		Hermes = new MessengerService();

		Routing.RegisterRoute("settings", typeof(SettingsPage));
	}

	public MessengerService Hermes { get; set; }  
}
