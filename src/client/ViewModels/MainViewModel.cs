using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace VolumeMixer.ViewModels;

public class MainViewModel
{
    private double channel1Volume;
    private double channel2Volume;
    private double channel3Volume;
    private double channel4Volume;

    public Command ConnectCommand { get; set; }
    public Command<string> MuteCommand { get; set; }

    public MainViewModel()
    {
        ConnectCommand = new Command(OnConnect);
        MuteCommand = new Command<string>(OnMute);
    }

    void OnMute(string channel)
    {
        (App.Current as App).Hermes.Publish("mute",$"{channel}");
    }

    private void OnConnect()
    {
        Shell.Current.GoToAsync("settings");
    }

    public double Channel1Volume
    {
        get => channel1Volume;
        set
        {
            channel1Volume = value;
            ChangeVolume(1, value);
        }
    }
    
    public double Channel2Volume
    {
        get => channel2Volume;
        set
        {
            channel2Volume = value;
            ChangeVolume(2, value);
        }
    }

    public double Channel3Volume
    {
        get => channel3Volume;
        set
        {
            channel3Volume = value;
            ChangeVolume(3, value);
        }
    }

    public double Channel4Volume
    {
        get => channel4Volume;
        set
        {
            channel4Volume = value;
            ChangeVolume(4, value);
        }
    }

    async Task ChangeVolume(int channel, double volume)
    {
        Debug.WriteLine($"channel: {channel} | volume: {volume}");
        await (App.Current as App).Hermes.Publish($"volume/{channel}", $"{volume}");
    }
}