namespace VolumeMixer.ViewModels;

public class SettingsViewModel
{
    public Command CloseCommand { get; set; }

    public SettingsViewModel()
    {
        CloseCommand = new Command(OnClose);
    }

    private void OnClose()
    {
        Shell.Current.GoToAsync("..");
    }
}